using System;
using System.Linq;
using System.Threading.Tasks;
using QuizzzClient.DataAccess.Interfaces;
using MongoDB.Driver;
using QuizzzClient.Entities;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using QuizzzClient.DataAccess.Exceptions;

namespace QuizzzClient.DataAccess.MongoDb
{
    public class MongoRepository<T> : IRepository<T>
        where T: class, MongoEntity
    {
        private IMongoDatabase db;
        private string collectionName;
        private IMongoCollection<T> collection;

        public MongoRepository(IMongoDatabase db, string collectionName) {
            this.db = db;
            this.collectionName = collectionName;
            this.collection = this.db.GetCollection<T>(collectionName);
        }

        public void Add(T item) {
            try {
                collection.InsertOne(item);
            } catch {
                throw new DatabaseConnectException();
            }
        }

        public T Find(object id) {
            try {
                var item = collection
                    .Find(Builders<T>.Filter.Eq("_id", ObjectId.Parse(id.ToString())))
                    .FirstOrDefault();
                return item;
            } catch {
                throw new DatabaseConnectException();
            }
        }

        public async Task<T> FindAsync(object id) {
            try {
                var searchResult = await collection
                    .FindAsync(Builders<T>.Filter.Eq("_id", ObjectId.Parse(id.ToString())));
                return searchResult.FirstOrDefault();
            } catch {
                throw new DatabaseConnectException();
            }
        }

        public IQueryable<T> GetAll() {
            try {
                return collection.AsQueryable();
            } catch {
                throw new DatabaseConnectException();
            }
        }

        public void Remove(object id) {
            try {
                collection.DeleteOne<T>(item => item.Id == id.ToString());
            } catch {
                throw new DatabaseConnectException();
            }
        }

        public void Update(T item) {
            try {
                collection.ReplaceOne(Builders<T>.Filter.Eq("_id", ObjectId.Parse(item.Id)), item);
            } catch {
                throw new DatabaseConnectException();
            }
        }

        public IQueryable<T> Where(Func<T, bool> predicate) {
            try {
                return collection
                    .AsQueryable()
                    .Where(predicate)
                    .AsQueryable();
            } catch {
                throw new DatabaseConnectException();
            }
        }
    }
}
