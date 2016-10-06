using System;
using System.Threading.Tasks;
using QuizzzClient.Entities;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using QuizzzClient.DataAccess.Interfaces;
using DataAccess.MongoDb;

namespace QuizzzClient.DataAccess.MongoDb
{
    public class MongoContext : IUnitOfWork
    {
        IMongoDatabase database;

        private MongoRepository<Quizz> quizzRepo;

        public MongoContext(MongoDbFactory dbFactory) {
            database = dbFactory.Database;
        }

        public IRepository<Quizz> Quizzes {
            get {
                if (quizzRepo == null) {
                    quizzRepo = new MongoRepository<Quizz>(database, "quizzes");
                }
                return quizzRepo;
            }
        }

        public void Dispose() { }

        public void SaveChanges() {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync() {
            throw new NotImplementedException();
        }

        public IMongoClient GetClient() {
            return database.Client;
        }
    }
}
