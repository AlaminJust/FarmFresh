using FarmFresh.Application.Enums;
using FarmFresh.Domain.Entities.Products;
using FarmFresh.Domain.RepoInterfaces.Products;
using FarmFresh.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmFresh.Infrastructure.Repo.Repositories.Products
{
    public class DiscountRepository : BaseRepository<Discount>, IDiscountRepository
    {
        #region Properties
        private readonly EFDbContext _context;
        private readonly IProductRepository _productRepository;
        #endregion Properties

        #region Constructor
        public DiscountRepository(
                IProductRepository productRepository,
                EFDbContext context
            ) : base(context)
        {
            _productRepository = productRepository;
            _context = context;
        }
        #endregion Constructor

        #region Get
        public async Task<decimal> CalculateDiscountOfProductAsync(int productId)
        {
            Product product = await _productRepository.GetByIdAsync(productId);
            decimal offer = 0;

            if (product is null)
            {
                return offer;
            }

            var discount = await GetByIdAsync(product.DiscountId ?? 0);

            if (discount is not null)
            {
                switch (discount.DiscountType)
                {
                    case DiscountType.Percentage:
                        offer = product.Price * discount.DiscountValue / 100;
                        break;
                    case DiscountType.Fixed:
                        offer = discount.DiscountValue;
                        break;
                    default:
                        break;
                }
            }

            return offer;
        }
        #endregion Get
    }
}
