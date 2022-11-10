using FarmFresh.Application.Dto.Request.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmFresh.Application.Interfaces.Services.Users
{
    public interface IUserService
    {
        #region Save
        Task AddAsync(UserRequest userRequest);
        #endregion Save
    }
}
