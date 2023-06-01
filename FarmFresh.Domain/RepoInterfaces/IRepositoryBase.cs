using System.Linq.Expressions;

namespace FarmFresh.Domain.RepoInterfaces
{
    public interface IBaseRepository<T>
    {
        #region Get
        Task<T> GetByIdAsync(int id);
        Task<IQueryable<T>> GetAllAsync();
        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression);

        public Task<IQueryable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression);

        #endregion Get
        
        #region Save
        Task<T> AddAsync(T entity);
        Task SaveChangesAsync();
        #endregion Save

        #region Update
        Task UpdateAsync(T entity);
        #endregion Update

        #region Delete
        Task DeleteAsync(T entity);
        #endregion Delete
    }
}
