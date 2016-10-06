using Microsoft.AspNetCore.Identity;
using QuizzzClient.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using QuizzzClient.DataAccess.Interfaces;
using MongoDB.Driver;
using DataAccess.MongoDb;
using QuizzzClient.Web.Identity.Entities;

namespace QuizzzClient.Web.Identity
{
    public class MongoUserStore : IUserStore<User>, IUserPasswordStore<User>
    {
        private IMongoDatabase db;
        private IMongoCollection<User> users;
        public MongoUserStore(MongoDbFactory dbFactory) {
            this.db = dbFactory.Database;
            users = db.GetCollection<User>("users");
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken) {
            await users.InsertOneAsync(user, cancellationToken: cancellationToken);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken) {
            await users.DeleteOneAsync(u => u.Id == user.Id, cancellationToken);
            return IdentityResult.Success;
        }

        public void Dispose() { }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken) {
            var result = await users.FindAsync(u => u.Id == userId, cancellationToken: cancellationToken);
            return result.FirstOrDefault();
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken) {
            var result = await users.FindAsync(u => u.NormalizedUserName == normalizedUserName, cancellationToken: cancellationToken);
            return result.FirstOrDefault();
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken) {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken) {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken) {
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken) {
            return Task.FromResult(user.UserName);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken) {
            return Task.FromResult(user.HasPassword());
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken) {
            return Task.Run(async () => {
                user.NormalizedUserName = normalizedName;
                var filter = Builders<User>.Filter.Eq(u => u.Id, user.Id);
                var update = Builders<User>.Update
                    .Set(u => u.NormalizedUserName, normalizedName);
                var result = await users.UpdateOneAsync(filter, update);
            });
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken) {
            return Task.Run(async () => {
                user.PasswordHash = passwordHash;
                var filter = Builders<User>.Filter.Eq(u => u.Id, user.Id);
                var update = Builders<User>.Update
                    .Set(u => u.PasswordHash, passwordHash);
                var result = await users.UpdateOneAsync(filter, update);
            });
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken) {
            return Task.Run(async () => {
                user.UserName = userName;
                var filter = Builders<User>.Filter.Eq(u => u.Id, user.Id);
                var update = Builders<User>.Update
                    .Set(u => u.UserName, userName);
                var result = await users.UpdateOneAsync(filter, update);
            });
        }

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken) {
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
