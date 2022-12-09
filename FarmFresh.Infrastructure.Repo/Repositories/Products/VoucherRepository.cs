using FarmFresh.Domain.Entities.Products;
using FarmFresh.Domain.RepoInterfaces.Products;
using FarmFresh.Infrastructure.Data.DbContexts;

namespace FarmFresh.Infrastructure.Repo.Repositories.Products
{
    public class VoucherRepository : BaseRepository<Voucher>, IVoucherRepository
    {
        public VoucherRepository(EFDbContext context) : base(context)
        {
        }
    }
}
