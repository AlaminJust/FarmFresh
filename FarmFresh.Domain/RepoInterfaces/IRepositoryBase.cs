using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FarmFresh.Domain.RepoInterfaces
{
    public interface IBaseRepository<T>
    {
        #region Get
        Task<T> GetByIdAsync(int id);
        Task<IQueryable<T>> GetAllAsync();
        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression);

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
