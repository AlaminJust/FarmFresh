using FarmFresh.Domain.Entities.Users;
using FarmFresh.Domain.RepoInterfaces.Users;
using FarmFresh.Infrastructure.Data.DbContexts;

namespace FarmFresh.Infrastructure.Repo.Repositories.Users
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(EFDbContext context) : base(context)
        {
        }
    }
}
