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
using Microsoft.AspNetCore.Authorization;

namespace QuizzzClient.Web.Controllers.Api
{
    [Authorize]
    [Route("api/quiz")]
    public class QuizController : Controller
    {
        private IUnitOfWork db;

        public QuizController(IUnitOfWork db) {
            this.db = db;
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormFile file) {
            string fileContent;
            Quiz quiz;

            using (var stream = file.OpenReadStream()) {
                var streamReader = new StreamReader(stream);
                fileContent = await streamReader.ReadToEndAsync();
                streamReader.Dispose();
            }

            if (IsJson(fileContent)) {
                try {
                    quiz = JsonConvert.DeserializeObject<Quiz>(fileContent);
                    AddQuizToDb(quiz);
                } catch (JsonException) {
                    return StatusCode(500);
                }

            } else if (IsXml(fileContent)) {
                try {
                    var doc = new XDocument(fileContent);
                    string jsonText = JsonConvert.SerializeXNode(doc);
                    quiz = JsonConvert.DeserializeObject<Quiz>(fileContent);
                    AddQuizToDb(quiz);
                } catch (JsonException) {
                    return StatusCode(500);
                }
            } else {
                return StatusCode(500);
            }

            Debug.Write("created");
            return Created("api/quiz", 0);
        }

        [HttpGet("previews/{count}")]
        public IActionResult GetPreviews(int count) {
            var quizzesStats = db.QuizzesStats.GetAll()
                .OrderByDescending(s => s.AttemptsCount)
                .AsQueryable();
            
            if (count != 0) {
                quizzesStats = quizzesStats.Take(count);
            }

            var quizPreviews = new List<QuizPreviewViewModel>(quizzesStats.Count());
            foreach (var stats in quizzesStats) {
                var quiz = db.Quizzes.Find(stats.Id);
                quizPreviews.Add(new QuizPreviewViewModel {
                    Id = stats.Id,
                    AttemptsCount = stats.AttemptsCount,
                    PassesCount = stats.PassesCount,
                    Name = quiz.Name,
                    Category = quiz.Category,
                    CountOfQuestions = quiz.Questions.Count()
                });
            }

            return Json(quizPreviews);
        }

        [HttpGet("{id}")]
        public IActionResult GetQuiz(string id) {
            var quiz = db.Quizzes.Find(id);
            if (quiz == null) {
                return StatusCode(500);
            }

            var viewModel = new QuizViewModel {
                Id = quiz.Id,
                Name = quiz.Name,
                Category = quiz.Category,
                Questions = quiz.Questions.Select(q => new QuestionViewModel {
                    Id = q.Id,
                    QuestionBody = q.QuestionBody,
                    Answers = q.Answers.Select(a => a.AnswerBody)
                })
            };

            return Json(viewModel);
        }

        [HttpPost("accept")]
        public IActionResult AcceptQuiz([FromBody]AcceptQuizViewModel data) {
            if (data != null) {
                var quiz = db.Quizzes.Find(data.QuizId);

                var correctQuestionsCount = 0;
                for (int i = 0; i < quiz.Questions.Count(); i++) {
                    var question = quiz.Questions.ElementAt(i);
                    var isQuestionCorrect = true;

                    for (int j = 0; j < question.Answers.Count(); j++) {
                        var answer = question.Answers.ElementAt(j);
                        if (answer.isCorrect && !data.Answers.ElementAt(i).Contains(j) ||
                            !answer.isCorrect && data.Answers.ElementAt(i).Contains(j)) {

                            isQuestionCorrect = false;
                        }
                    }

                    if (isQuestionCorrect) {
                        correctQuestionsCount++;
                    }
                }

                // Save stats in db and return result

                var quizStats = db.QuizzesStats.Find(quiz.Id);
                quizStats.AttemptsCount++;

                var isQuizPassed = isTestPassed(correctQuestionsCount, quiz.Questions.Count());

                if (isQuizPassed) {
                    quizStats.PassesCount++;
                }

                db.QuizzesStats.Update(quizStats);

                var result = new AcceptQuizResultViewModel {
                    Id = quiz.Id,
                    AllQuestionsCount = quiz.Questions.Count(),
                    PassedQuestionsCount = correctQuestionsCount,
                    Success = isQuizPassed
                };

                return Json(result);
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

        private void AddQuizToDb(Quiz quizz) {
            db.Quizzes.Add(quizz);
            db.QuizzesStats.Add(new QuizStats {
                Id = quizz.Id,
                AttemptsCount = 0,
                PassesCount = 0
            });
        }

        private bool isTestPassed(int passedQuestionsCount, int allQuestionsCount) {
            const double LIMIT = 0.5;

            if ((double)passedQuestionsCount / allQuestionsCount > LIMIT) {
                return true;
            }

            return false;
        }

        #endregion
    }
}