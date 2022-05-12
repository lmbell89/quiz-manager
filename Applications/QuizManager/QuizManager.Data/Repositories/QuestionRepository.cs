using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using QuizManager.Data.Entities;

namespace QuizManager.Data.Repositories
{
    public class QuestionRepository : GenericRepository<Question>
    {
        public QuestionRepository(QuizContext context) : base(context)
        {
        }

        public override Question Find(int id)
        {
            return _dbSet.Include(question => question.Answers)
                .FirstOrDefault(quiz => quiz.Id == id);
        }
    }
}
