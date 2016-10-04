using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzzClient.Entities
{
    public class QuestionStats : MongoEntity
    {
        public ObjectId Id { get; set; }
        public Question Question { get; set; }
        public int PassesCount { get; set; }
        public int AttemptsCount { get; set; }
    }
}
