using FarmFresh.Domain.Entities.Products;
using FarmFresh.Domain.RepoInterfaces.Products;
using FarmFresh.Infrastructure.Data.DbContexts;

namespace FarmFresh.Infrastructure.Repo.Repositories.Products
{
    public class OrderItemRepository : BaseRepository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(EFDbContext context) : base(context)
        {
        }
    }
}
