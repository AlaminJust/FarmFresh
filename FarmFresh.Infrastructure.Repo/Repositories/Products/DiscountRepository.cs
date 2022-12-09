using FarmFresh.Domain.Entities.Products;
using FarmFresh.Domain.RepoInterfaces.Products;
using FarmFresh.Infrastructure.Data.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmFresh.Infrastructure.Repo.Repositories.Products
{
    public class DiscountRepository : BaseRepository<Discount>, IDiscountRepository
    {
        public DiscountRepository(EFDbContext context) : base(context)
        {
        }
    }
}
