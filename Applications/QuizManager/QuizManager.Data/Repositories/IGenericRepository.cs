using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace QuizManager.Data.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        public IEnumerable<T> Get(
            int? startIndex = 0, 
            int? count = 10, 
            Expression<Func<T, bool>> predicate = null);
        public T Find(int id);
        public T Create(T entity);
        public void Update(T entity);
        public void Delete(T entity);
        public void Save();
    }
}
