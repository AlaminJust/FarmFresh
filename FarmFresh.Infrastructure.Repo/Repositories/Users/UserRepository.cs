using FarmFresh.Domain.Entities.Users;
using FarmFresh.Domain.RepoInterfaces.Users;
using FarmFresh.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace FarmFresh.Infrastructure.Repo.Repositories.Users
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly EFDbContext _context;

        public UserRepository(EFDbContext context) : base(context)
        {
            _context = context;
        }

        #region Get
        public async Task<User> GetByUserNameAsync(string userName)
        {
            var user = await _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            return user;
        }
        #endregion Get
    }
}
