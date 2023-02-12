using FarmFresh.Domain.Entities.Products;
using FarmFresh.Domain.RepoInterfaces.Products;
using FarmFresh.Infrastructure.Data.DbContexts;

namespace FarmFresh.Infrastructure.Repo.Repositories.Products
{
    public class PaymentRepository : BaseRepository<PaymentDetail>, IPaymentRepository
    {
        public PaymentRepository(EFDbContext context) : base(context)
        {
        }
    }
}
