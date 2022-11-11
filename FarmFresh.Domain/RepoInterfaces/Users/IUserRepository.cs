using FarmFresh.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmFresh.Domain.RepoInterfaces.Users
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        #region Get
        Task<User> GetByUserNameAsync(string userName);
        #endregion Get
    }
}
