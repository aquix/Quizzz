using System;
using System.Threading.Tasks;
using QuizzzClient.Entities;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using QuizzzClient.DataAccess.Interfaces;

namespace QuizzzClient.DataAccess.MongoDb
{
    public class MongoContext : IUnitOfWork
    {
        MongoClient client;
        IMongoDatabase database;
        MongoGridFS gridFS;

        private MongoRepository<Quizz> quizzRepo;
        private MongoRepository<User> userRepo;

        public MongoContext(string connectionString) {
            var connection = new MongoUrlBuilder(connectionString);

            client = new MongoClient(connectionString);
            database = client.GetDatabase(connection.DatabaseName);
            gridFS = new MongoGridFS(
                new MongoServer(
                    new MongoServerSettings { Server = connection.Server }),
                connection.DatabaseName,
                new MongoGridFSSettings()
            );
        }

        public IRepository<Quizz> Quizzes {
            get {
                if (quizzRepo == null) {
                    quizzRepo = new MongoRepository<Quizz>(database, "quizzes");
                }
                return quizzRepo;
            }
        }

        public IRepository<User> Users {
            get {
                if (userRepo == null) {
                    userRepo = new MongoRepository<User>(database, "users");
                }
                return userRepo;
            }
        }

        public void Dispose() {
            throw new NotImplementedException();
        }

        public void SaveChanges() {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync() {
            throw new NotImplementedException();
        }

        public MongoClient GetClient() {
            return client;
        }
    }
}
