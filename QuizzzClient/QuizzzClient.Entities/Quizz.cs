using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace QuizzzClient.Entities
{
    public class Quizz : MongoEntity
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public IEnumerable<Question> Questions { get; set; }
    }
}
