using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzzClient.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionBody { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
    }
}
