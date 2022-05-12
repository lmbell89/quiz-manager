using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using QuizManager.Data.Entities;

namespace QuizManager.Data.Repositories
{
    public class QuizRepository : GenericRepository<Quiz>
    {
        public QuizRepository(QuizContext context) : base(context)
        {
        }

        public override Quiz Find(int id)
        {
            return _dbSet.Include(quiz => quiz.Questions)
                .ThenInclude(question => question.Answers)
                .FirstOrDefault(quiz => quiz.Id == id);
        }

        public override IEnumerable<Quiz> Get(int? startIndex = 0, int? count = 10, Expression<Func<Quiz, bool>> predicate = null)
        {
            IQueryable<Quiz> query = _dbSet.Include(q => q.Questions);

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (startIndex != null)
            {
                query = query.Skip((int)startIndex);
            }

            if (count != null && count > 0)
            {
                query = query.Take((int)count);
            }

            return query.ToList();
        }
    }
}
