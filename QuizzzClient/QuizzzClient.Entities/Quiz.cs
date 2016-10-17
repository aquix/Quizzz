using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace QuizzzClient.Entities
{
    public class Quiz : MongoEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int Time { get; set; }
        public string CategoryId { get; set; }
        public IEnumerable<Question> Questions { get; set; }

        // Stats
        public int PassesCount { get; set; }
        public int AttemptsCount { get; set; }
    }
}
