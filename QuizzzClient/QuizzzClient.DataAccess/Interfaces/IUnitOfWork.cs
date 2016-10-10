using QuizzzClient.Entities;
using System;
using System.Threading.Tasks;

namespace QuizzzClient.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Quiz> Quizzes { get; }
        IRepository<QuizStats> QuizzesStats { get; }
        IRepository<Category> Categories { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}