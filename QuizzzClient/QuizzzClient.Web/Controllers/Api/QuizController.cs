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
                return StatusCode(500);
            }

            using (var stream = file.OpenReadStream()) {
                using (var streamReader = new StreamReader(stream)) {
                    fileContent = await streamReader.ReadToEndAsync();
                    streamReader.Dispose();
                }
            }

            var result = quizService.AddQuiz(fileContent);

            if (result) {
                Debug.Write("created");
                return Created("api/quiz", 0);
            } else {
                return StatusCode(500);
            }
        }

        [HttpGet("previews/{count}")]
        public IActionResult GetPreviews(int count, string category) {
            var quizPreviews = quizService.GetPreviews(count, category);
            return Json(quizPreviews);
        }

        [HttpGet("{id}")]
        public IActionResult GetQuiz(string id) {
            var quizViewModel = quizService.GetQuiz(id);
            return Json(quizViewModel);
        }

        [HttpPost("accept")]
        public async Task<IActionResult> AcceptQuiz([FromBody]AcceptQuizViewModel data) {
            if (data == null) {
                StatusCode(500);
            }

            Debug.Write(User.Identity.Name);
            var result = await quizService.AcceptQuiz(data, User.Identity.Name);

            if (result == null) {
                StatusCode(500);
            }

            return Json(result);
        }

        [HttpGet("stats")]
        public async Task<IEnumerable<QuizBestResult>> GetUserStats() {
            var userName = User.Identity.Name;
            return await quizService.GetUserStats(userName);
        }
    }
}