using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace QuizManager.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly QuizContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(QuizContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual T Create(T entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public virtual void Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public virtual T Find(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual IEnumerable<T> Get(
            int? startIndex = 0, 
            int? count = 10, 
            Expression<Func<T, bool>> predicate = null)
        {
            IQueryable<T> query = _dbSet;

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

        public virtual void Save()
        {
            _context.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
