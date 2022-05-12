using System;
using QuizManager.Data.Entities;
using QuizManager.Data.Repositories;

namespace QuizManager.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly QuizContext _context;

        private QuizRepository _quizRepository;

        private QuestionRepository _questionRepository;

        private GenericRepository<Answer> _answerRepository;

        private bool disposed = false;

        public UnitOfWork(QuizContext context)
        {
            _context = context;
        }

        public IGenericRepository<Quiz> QuizRepository {
            get
            {
                if (_quizRepository == null)
                {
                    _quizRepository = new QuizRepository(_context);
                }
                return _quizRepository;
            }
        }
        public IGenericRepository<Question> QuestionRespository { 
            get
            {
                if (_questionRepository == null)
                {
                    _questionRepository = new QuestionRepository(_context);
                }
                return _questionRepository;
            }
        }
        public IGenericRepository<Answer> AnswerRepository { 
            get
            {
                if (_answerRepository == null)
                {
                    _answerRepository = new GenericRepository<Answer>(_context);
                }
                return _answerRepository;
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                _context.Dispose();
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
