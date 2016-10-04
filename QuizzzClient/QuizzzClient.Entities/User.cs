using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace QuizzzClient.Entities
{
    public class User : MongoEntity
    {
        public ObjectId Id { get; set; }
    }
}
