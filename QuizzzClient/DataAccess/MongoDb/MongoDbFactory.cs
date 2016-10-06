using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.MongoDb
{
    public class MongoDbFactory
    {
        private MongoClient client;
        private IMongoDatabase db;
        private MongoGridFS gridFS;
        private string connectionString;

        public MongoDbFactory(string connectionString) {
            this.connectionString = connectionString;
        }

        public IMongoDatabase Database {
            get {
                if (db == null) {
                    var connection = new MongoUrlBuilder(connectionString);

                    client = new MongoClient(connectionString);
                    db = client.GetDatabase(connection.DatabaseName);
                    gridFS = new MongoGridFS(
                        new MongoServer(
                            new MongoServerSettings { Server = connection.Server }),
                        connection.DatabaseName,
                        new MongoGridFSSettings()
                    );
                }

                return db;
            }
        }
    }
}
