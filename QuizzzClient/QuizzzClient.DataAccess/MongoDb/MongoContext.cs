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

        private MongoRepository<Quiz> quizRepo;
        private MongoRepository<Category> categoryRepo;

        public MongoContext(MongoDbFactory dbFactory) {
            database = dbFactory.Database;
        }

        public IRepository<Quiz> Quizzes {
            get {
                if (quizRepo == null) {
                    quizRepo = new MongoRepository<Quiz>(database, "quizzes");
                }
                return quizRepo;
            }
        }

        public IRepository<Category> Categories {
            get {
                if (categoryRepo == null) {
                    categoryRepo = new MongoRepository<Category>(database, "categories");
                }
                return categoryRepo;
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
