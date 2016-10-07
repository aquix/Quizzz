using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;
using QuizzzClient.DataAccess.Interfaces;
using Newtonsoft.Json;
using QuizzzClient.Entities;
using System.Linq;
using System.Xml.Linq;
using QuizzzClient.Web.Models.ApiViewModels;
using System.Collections.Generic;

namespace QuizzzClient.Web.Controllers.Api
{
    [Route("api/quizz")]
    public class QuizzController : Controller
    {
        private IUnitOfWork db;

        public QuizzController(IUnitOfWork db) {
            this.db = db;
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormFile file) {
            string fileContent;
            Quizz quizz;

            using (var stream = file.OpenReadStream()) {
                var streamReader = new StreamReader(stream);
                fileContent = await streamReader.ReadToEndAsync();
                streamReader.Dispose();
            }

            if (IsJson(fileContent)) {
                try {
                    quizz = JsonConvert.DeserializeObject<Quizz>(fileContent);
                    AddQuizzToDb(quizz);
                } catch (JsonException) {
                    return StatusCode(500);
                }

            } else if (IsXml(fileContent)) {
                try {
                    var doc = new XDocument(fileContent);
                    string jsonText = JsonConvert.SerializeXNode(doc);
                    quizz = JsonConvert.DeserializeObject<Quizz>(fileContent);
                    AddQuizzToDb(quizz);
                } catch (JsonException) {
                    return StatusCode(500);
                }
            } else {
                return StatusCode(500);
            }

            Debug.Write("created");
            return Created("api/quizz", 0);
        }

        [HttpGet("previews/{count}")]
        public IActionResult GetPreviews(int count) {
            var popularQuizzesStats = db.QuizzesStats.GetAll()
                .OrderByDescending(s => s.AttemptsCount)
                .AsQueryable();
            
            if (count != 0) {
                popularQuizzesStats = popularQuizzesStats.Take(count);
            }

            var popularQuizzPreviews = new List<QuizzPreviewViewModel>(popularQuizzesStats.Count());
            foreach (var stats in popularQuizzesStats) {
                var quizz = db.Quizzes.Find(stats.Id);
                popularQuizzPreviews.Add(new QuizzPreviewViewModel {
                    Id = stats.Id,
                    AttemptsCount = stats.AttemptsCount,
                    PassesCount = stats.PassesCount,
                    Name = quizz.Name,
                    Category = quizz.Category,
                    CountOfQuestions = quizz.Questions.Count()
                });
            }

            return Json(popularQuizzPreviews);
        }

        [HttpGet("{id}")]
        public IActionResult GetQuizz(string id) {
            var quizz = db.Quizzes.Find(id);
            if (quizz == null) {
                return StatusCode(500);
            }

            var viewModel = new QuizzViewModel {
                Id = quizz.Id,
                Name = quizz.Name,
                Category = quizz.Category,
                Questions = quizz.Questions.Select(q => new QuestionViewModel {
                    Id = q.Id,
                    QuestionBody = q.QuestionBody,
                    Answers = q.Answers.Select(a => a.AnswerBody)
                })
            };

            return Json(viewModel);
        }

        [HttpPost("accept")]
        public IActionResult AcceptQuizz(AcceptQuizzViewModel data) {
            if (data != null) {
                return Json("good");
            } else {
                return StatusCode(500);
            }
        }

        #region Helpers

        private bool IsJson(string content) {
            if (content.First() == '{') {
                return true;
            }

            return false;
        }

        private bool IsXml(string content) {
            if (content.First() == '<') {
                return true;
            }

            return false;
        }

        private void AddQuizzToDb(Quizz quizz) {
            db.Quizzes.Add(quizz);
            db.QuizzesStats.Add(new QuizzStats {
                Id = quizz.Id,
                AttemptsCount = 0,
                PassesCount = 0
            });
        }

        #endregion
    }
}