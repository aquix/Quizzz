using Newtonsoft.Json;
using QuizzzClient.DataAccess.Interfaces;
using QuizzzClient.Entities;
using QuizzzClient.Web.Models.ApiViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QuizzzClient.Web.Services
{
    public class QuizService
    {
        private IUnitOfWork db;

        public QuizService(IUnitOfWork db) {
            this.db = db;
        }

        public bool AddQuiz(string data) {
            QuizData quizData;

            try {
                if (IsJson(data)) {
                    quizData = JsonConvert.DeserializeObject<QuizData>(data);
                    AddQuizToDb(quizData);
                } else if (IsXml(data)) {
                    var doc = new XDocument(data);
                    string jsonText = JsonConvert.SerializeXNode(doc);
                    quizData = JsonConvert.DeserializeObject<QuizData>(data);
                    AddQuizToDb(quizData);
                } else {
                    return false;
                }
            } catch (JsonException) {
                return false;
            }

            return true;
        }

        public IEnumerable<QuizPreviewViewModel> GetPreviews(int count) {
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
                    Author = quiz.Author,
                    Category = db.Categories.Where(c => c.Id == quiz.CategoryId).FirstOrDefault()?.Name,
                    CountOfQuestions = quiz.Questions.Count()
                });
            }

            return quizPreviews;
        }

        public AcceptQuizResultViewModel AcceptQuiz(AcceptQuizViewModel data) {
            Quiz quiz;
            try {
                quiz = db.Quizzes.Find(data.QuizId);
            } catch (Exception) {
                return null;
            }

            if (quiz.Questions.Count() != data.Answers.Count()) {
                return null;
            }

            var correctQuestionsCount = 0;
            for (int i = 0; i < quiz.Questions.Count(); i++) {
                var question = quiz.Questions.ElementAt(i);
                var isQuestionCorrect = true;

                for (int j = 0; j < question.Answers.Count(); j++) {
                    var answer = question.Answers.ElementAt(j);
                    if (answer.IsCorrect && !data.Answers.ElementAt(i).Contains(j) ||
                        !answer.IsCorrect && data.Answers.ElementAt(i).Contains(j)) {

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

            var isQuizPassed = IsTestPassed(correctQuestionsCount, quiz.Questions.Count());

            if (isQuizPassed) {
                quizStats.PassesCount++;
            }

            db.QuizzesStats.Update(quizStats);

            return new AcceptQuizResultViewModel {
                Id = quiz.Id,
                AllQuestionsCount = quiz.Questions.Count(),
                PassedQuestionsCount = correctQuestionsCount,
                Success = isQuizPassed
            };
        }

        public QuizViewModel GetQuiz(string id) {
            var quiz = db.Quizzes.Find(id);
            if (quiz == null) {
                return null;
            }

            var quizViewModel = new QuizViewModel {
                Id = quiz.Id,
                Name = quiz.Name,
                Author = quiz.Author,
                Category = db.Categories.Where(c => c.Id == quiz.CategoryId).FirstOrDefault()?.Name,
                Questions = quiz.Questions.Select(q => new QuestionViewModel {
                    Id = q.Id,
                    QuestionBody = q.QuestionBody,
                    Answers = q.Answers.Select(a => a.AnswerBody)
                })
            };

            return quizViewModel;
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

        private void AddQuizToDb(QuizData quizData) {
            Quiz quiz = new Quiz {
                Author = quizData.Author,
                Name = quizData.Name,
                Questions = quizData.Questions
            };
             
            var categoryName = quizData.Category;
            var existingCategory = db.Categories.Where(c => c.Name == categoryName).FirstOrDefault();

            var categoryId = "";

            if (existingCategory == null) {
                var newCategory = new Category {
                    Name = categoryName
                };
                db.Categories.Add(newCategory);
                categoryId = newCategory.Id;
            } else {
                categoryId = existingCategory.Id;
            }

            quiz.CategoryId = categoryId;

            db.Quizzes.Add(quiz);
            db.QuizzesStats.Add(new QuizStats {
                Id = quiz.Id,
                AttemptsCount = 0,
                PassesCount = 0
            });
        }

        private bool IsTestPassed(int passedQuestionsCount, int allQuestionsCount) {
            const double LIMIT = 0.5;

            if ((double)passedQuestionsCount / allQuestionsCount > LIMIT) {
                return true;
            }

            return false;
        }

        #endregion
    }
}
