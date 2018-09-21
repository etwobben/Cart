using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Data;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DatabaseContext _dbContext;

        protected BaseRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            var query = _dbContext.Set<T>();

            return await Includes(query).SingleOrDefaultAsync(t => t.Id == id);
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            var query = _dbContext.Set<T>();
            return await Includes(query).ToListAsync();
        }


        public virtual async Task<List<T>> Where(Expression<Func<T, bool>> expression)
        {
            var query = _dbContext.Set<T>().Where(expression);

            return await Includes(query).ToListAsync();
        }

        public async Task<T> InsertAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        protected virtual IQueryable<T> Includes(IQueryable<T> query)
        {
            return query;
        }
    }
}
