using QuizzzClient.Entities;
using System;
using System.Threading.Tasks;

namespace QuizzzClient.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Quizz> Quizzes { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}