using FarmFresh.Core.Enums;
using FarmFresh.Domain.Entities.Users;
using FarmFresh.Domain.RepoInterfaces.Users;
using FarmFresh.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace FarmFresh.Infrastructure.Repo.Repositories.Users
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        #region Properties
        private readonly EFDbContext _context;
        #endregion Properties
        
        #region Constructor
        public RoleRepository(EFDbContext context) : base(context)
        {
            _context = context;
        }
        #endregion Constructor
        
        #region Get
        public async Task<Role> GetByTypeAsync(RoleType roleType)
        {
            return await _context.Roles.Where(x => x.RoleType == roleType).FirstOrDefaultAsync();
        }

        public async Task<IList<string>> GetRoleNamesByUserIdAsync(int userId)
        {
            var roles = (from ur in _context.UserRoles
                        join r in _context.Roles on ur.RoleId equals r.Id
                        where (ur.UserId == userId)
                        select r.Name).ToListAsync();

            return await roles;
        }
        #endregion Get
    }
}
