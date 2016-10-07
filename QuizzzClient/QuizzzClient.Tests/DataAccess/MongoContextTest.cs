using QuizzzClient.DataAccess.MongoDb;
using QuizzzClient.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace QuizzzClient.Tests.DataAccess
{
    public class MongoContextTest : IClassFixture<ContextFixture>
    {
        MongoContext context;

        public MongoContextTest(ContextFixture fixture) {
            this.context = fixture.Context;
        }

        [Fact]
        public void ShouldBeEmptyOnStart() {
            var quizzes = context.Quizzes.GetAll();

            Assert.Equal(0, quizzes.Count());
        }

        [Fact]
        public void AllowsToAddElements() {
            Seed();

            var quizResult1 = context.Quizzes.Where(q => q.Category == "Fun").FirstOrDefault();

            Assert.Equal(2, context.Quizzes.GetAll().Count());
            Assert.Equal("Who are you in Winx", quizResult1.Name);
        }

        [Fact]
        public void AllowToDeleteElements() {
            Seed();

            var quiz = context.Quizzes.Where(q => q.Name == "What is your favorite food").FirstOrDefault();
            context.Quizzes.Remove(quiz.Id);

            Assert.Null(context.Quizzes.Where(q => q.Name == "What is your favorite food").FirstOrDefault());
            Assert.Equal(1, context.Quizzes.GetAll().Count());
        }

        private void Seed() {
            var quiz1 = new Quiz {
                Category = "Other",
                Name = "What is your favorite food",
                Questions = new List<Question> {
                    new Question {
                        QuestionBody = "Say smth",
                        Answers = new List<Answer> {
                            new Answer { AnswerBody = "Meow", isCorrect = true },
                            new Answer { AnswerBody = "Gav", isCorrect = false },
                            new Answer { AnswerBody = "Blabla", isCorrect = false },
                        }
                    }
                }
            };

            var quiz2 = new Quiz {
                Category = "Fun",
                Name = "Who are you in Winx",
                Questions = new List<Question> {
                    new Question {
                        QuestionBody = "Say smth",
                        Answers = new List<Answer> {
                            new Answer { AnswerBody = "Meow", isCorrect = true },
                            new Answer { AnswerBody = "Gav", isCorrect = true },
                            new Answer { AnswerBody = "Blabla", isCorrect = false },
                        }
                    }
                }
            };

            context.Quizzes.Add(quiz1);
            context.Quizzes.Add(quiz2);
        }
    }
}
