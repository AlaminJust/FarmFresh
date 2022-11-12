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
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        #region Properties
        private readonly EFDbContext _context;
        #endregion Properties
        
        #region Constructor
        public ProductRepository(EFDbContext context) : base(context)
        {
            _context = context;
        }
        #endregion Constructor
    }
}
