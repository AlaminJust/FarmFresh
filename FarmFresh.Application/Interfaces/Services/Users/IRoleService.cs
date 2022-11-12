using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmFresh.Application.Interfaces.Services.Users
{
    public interface IRoleService
    {
        Task<IList<string>> GetRoleNamesByUserIdAsync(int userId);
    }
}
