using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;

namespace Domain.Services
{
    public abstract class BaseService<T> : IService<T> where T : BaseEntity
    {
        protected IRepository<T> Repository { get; }

        protected BaseService(IRepository<T> repository)
        {
            Repository = repository;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Repository.GetAllAsync();
        }

        public async Task<IEnumerable<T>> Where(Expression<Func<T, bool>> expression)
        {
            return await Repository.Where(expression);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await Repository.GetByIdAsync(id);
        }

        public async Task<T> InsertAsync(T entity)
        {
            return await Repository.InsertAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            await Repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            await Repository.DeleteAsync(entity);
        }
    }
}
