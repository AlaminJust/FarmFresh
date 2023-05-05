using FarmFresh.Domain.Entities.Users;
using FarmFresh.Domain.RepoInterfaces.Users;
using FarmFresh.Infrastructure.Data.DbContexts;

namespace FarmFresh.Infrastructure.Repo.Repositories.Users
{
    public class LocationRepository : BaseRepository<Location>, ILocationRepository
    {
        public LocationRepository(EFDbContext context) : base(context)
        {
        }
    }
}
