using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzzClient.Entities
{
    public interface MongoEntity
    {
        ObjectId Id { get; set; }
    }
}
