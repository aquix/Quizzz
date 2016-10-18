using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using System.IO;
using QuizzzClient.DataAccess.Interfaces;
using System.Linq;
using QuizzzClient.Web.Models.ApiViewModels;
using Microsoft.AspNetCore.Authorization;
using QuizzzClient.Web.Services;
using System.Collections;
using QuizzzClient.Entities;
using System.Collections.Generic;
using System;

namespace QuizzzClient.Web.Controllers.Api
{
    [Authorize]
    [Route("api/quiz")]
    public class QuizController : Controller
    {
        private IUnitOfWork db;
        private QuizService quizService;

        public QuizController(IUnitOfWork db, QuizService quizService) {
            this.db = db;
            this.quizService = quizService;
            
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormFile file) {
            string fileContent;

            if (file == null) {
                return StatusCode(400, "File not exists");
            }

            using (var stream = file.OpenReadStream()) {
                using (var streamReader = new StreamReader(stream)) {
                    fileContent = await streamReader.ReadToEndAsync();
                    streamReader.Dispose();
                }
            }

            try {
                quizService.AddQuiz(fileContent);
                return Created("api/quiz", 0);
            } catch (Exception e) {
                return StatusCode(400, e.Message);
            }
        }

        [HttpGet("previews/{count}")]
        public IActionResult GetPreviews(int count=0, int startFromIndex=0, string category="") {
            try {
                var quizPreviews = quizService.GetPreviews(count, startFromIndex, category);
                return Json(quizPreviews);
            } catch (Exception e) {
                return StatusCode(400, e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetQuiz(string id) {
            try {
                var quizViewModel = quizService.GetQuiz(id);
                return Json(quizViewModel);
            } catch (Exception e) {
                return NotFound(e.Message);
            }
        }

        [HttpPost("accept")]
        public async Task<IActionResult> AcceptQuiz([FromBody]AcceptQuizViewModel data) {
            try {
                var result = await quizService.AcceptQuiz(data, User.Identity.Name);
                return Json(result);
            } catch (Exception e) {
                return StatusCode(400, e.Message);
            }
        }

        [HttpGet("stats")]
        public async Task<IEnumerable<QuizBestResult>> GetUserStats() {
            var userName = User.Identity.Name;
            return await quizService.GetUserStats(userName);
        }
    }
}