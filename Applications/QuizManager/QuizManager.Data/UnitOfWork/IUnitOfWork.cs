using System;
using QuizManager.Data.Entities;
using QuizManager.Data.Repositories;

namespace QuizManager.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IGenericRepository<Quiz> QuizRepository { get; }
        public IGenericRepository<Question> QuestionRespository { get; }
        public IGenericRepository<Answer> AnswerRepository { get; }
        public void SaveChanges();
    }
}
