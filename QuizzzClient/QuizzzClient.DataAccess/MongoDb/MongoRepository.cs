using System;
using System.Linq;
using System.Threading.Tasks;
using QuizzzClient.DataAccess.Interfaces;
using MongoDB.Driver;
using QuizzzClient.Entities;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

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
            collection.InsertOne(item);
        }

        public T Find(object id) {
            var item = collection
                .Find(Builders<T>.Filter.Eq("_id", ObjectId.Parse(id.ToString())))
                .FirstOrDefault();
            return item;
        }

        public async Task<T> FindAsync(object id) {
            var searchResult = await collection
                .FindAsync(Builders<T>.Filter.Eq("_id", id.ToString()));
            return searchResult.FirstOrDefault();
        }

        public IQueryable<T> GetAll() {
            return collection.AsQueryable();
        }

        public void Remove(object id) {
            collection.DeleteOne<T>(item => item.Id == id.ToString());
        }

        public void Update(T item) {
            collection.ReplaceOne(Builders<T>.Filter.Eq("_id", item.Id.ToString()), item);
        }

        public IQueryable<T> Where(Func<T, bool> predicate) {
            return collection
                .AsQueryable()
                .Where(predicate)
                .AsQueryable();
        }
    }
}
