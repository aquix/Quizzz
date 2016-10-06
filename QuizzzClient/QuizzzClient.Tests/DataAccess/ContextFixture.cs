using DataAccess.MongoDb;
using MongoDB.Driver;
using QuizzzClient.DataAccess.MongoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzzClient.Tests.DataAccess
{
    public class ContextFixture
    {
        private string dbName;

        public MongoContext Context { get; set; }

        public IMongoDatabase Db {
            get {
                return Context.GetClient().GetDatabase(dbName);
            }
        }

        public ContextFixture() {
            dbName = "quizzzDbTest";
            Context = new MongoContext(new MongoDbFactory($"mongodb://localhost:27017/{dbName}"));
            Context.GetClient().DropDatabase("quizzzDbTest");
        }
    }
}
