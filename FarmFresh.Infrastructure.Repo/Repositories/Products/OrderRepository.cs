using FarmFresh.Domain.Entities.Products;
using FarmFresh.Domain.RepoInterfaces.Products;
using FarmFresh.Infrastructure.Data.DbContexts;

namespace FarmFresh.Infrastructure.Repo.Repositories.Products
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(EFDbContext context) : base(context)
        {
        }
    }
}
