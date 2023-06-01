using FarmFresh.Domain.Entities.Products;
using FarmFresh.Domain.RepoInterfaces.Products;
using FarmFresh.Infrastructure.Data.DbContexts;

namespace FarmFresh.Infrastructure.Repo.Repositories.Products;

public class ProductHistoryRepository: BaseRepository<ProductHistory>, IProductHistoryRepository
{
    public ProductHistoryRepository(EFDbContext context) : base(context)
    {
    }
}