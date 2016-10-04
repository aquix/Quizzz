using System;
using System.Linq;
using System.Threading.Tasks;
using QuizzzClient.DataAccess.Interfaces;
using MongoDB.Driver;
using QuizzzClient.Entities;
using MongoDB.Bson;

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
            return collection
                .Find(doc => doc.Id == new ObjectId(id.ToString()))
                .FirstOrDefault();
        }

        public async Task<T> FindAsync(object id) {
            var searchResult = await collection
                .FindAsync(doc => doc.Id == new ObjectId(id.ToString()));
            return searchResult.FirstOrDefault();
        }

        public IQueryable<T> GetAll() {
            return collection.AsQueryable();
        }

        public void Remove(object id) {
            collection.DeleteOne<T>(doc => doc.Id == new ObjectId(id.ToString()));
        }

        public void Update(T item) {
            collection.ReplaceOne(doc => doc.Id == item.Id, item);
        }

        public IQueryable<T> Where(Func<T, bool> predicate) {
            return collection
                .AsQueryable()
                .Where(predicate)
                .AsQueryable();
        }
    }
}
