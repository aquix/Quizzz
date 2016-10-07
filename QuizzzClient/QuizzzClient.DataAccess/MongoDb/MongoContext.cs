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
        private MongoRepository<QuizStats> quizzesStatsRepo;

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

        public IRepository<QuizStats> QuizzesStats {
            get {
                if (quizzesStatsRepo == null) {
                    quizzesStatsRepo = new MongoRepository<QuizStats>(database, "quizzesStats");
                }
                return quizzesStatsRepo;
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
