using FarmFresh.Domain.Entities;
using FarmFresh.Domain.RepoInterfaces;
using FarmFresh.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FarmFresh.Infrastructure.Repo.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        #region Properties
        private readonly EFDbContext _context;
        #endregion Properties
        
        #region Constructor
        public BaseRepository
            (
                EFDbContext context
            )
        {
            _context = context;
        }
        #endregion Constructor
        
        #region Get

        public Task<T> GetByIdAsync(int id)
        {
            return _context.Set<T>().FindAsync(id).AsTask();
        }

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression).AsNoTracking();
        }

        public Task<IQueryable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression)
        {
            var query = _context.Set<T>().Where(expression).AsNoTracking();
            return Task.FromResult(query);
        }

        public Task<IQueryable<T>> GetAllAsync()
        {
            return Task.FromResult(_context.Set<T>().AsNoTracking());
        }

        #endregion Get

        #region Save
        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }
        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        #endregion Save

        #region Update

        public Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            return Task.CompletedTask;
        }

        #endregion Update

        #region Delete
        public Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        #endregion Delete
    }
}
