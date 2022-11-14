using FarmFresh.Application.Models.Paginations;
using FarmFresh.Application.Models.Paginations.Products;
using FarmFresh.Domain.Entities.Products;
using FarmFresh.Domain.RepoInterfaces.Products;
using FarmFresh.Infrastructure.Data.DbContexts;

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

        #region Get
        public async Task<PaginationResponse<Product>> GetPaginatedProductsAsync(ProductPaginationRequest productPaginationRequest)
        {
            var products = _context.Products
                .Where(x => x.IsDeleted == false)
                .AsQueryable();

            if (!string.IsNullOrEmpty(productPaginationRequest.Search))
            {
                products = products.Where(x => x.Name.Contains(productPaginationRequest.Search));
            }

            if (productPaginationRequest.CategoryId is not null)
            {
                products = products.Where(x => x.CategoryId == productPaginationRequest.CategoryId);
            }
            
            if (productPaginationRequest.BrandId is not null)
            {
                products = products.Where(x => x.BrandId == productPaginationRequest.BrandId);
            }

            if (productPaginationRequest.MinPrice is not null)
            {
                products = products.Where(x => x.Price >= productPaginationRequest.MinPrice);
            }

            if (productPaginationRequest.MaxPrice is not null)
            {
                products = products.Where(x => x.Price <= productPaginationRequest.MaxPrice);
            }

            if (productPaginationRequest.Quantity is not null)
            {
                products = products.Where(x => x.Quantity >= productPaginationRequest.Quantity);
            }

            return await PaginationResponse<Product>.CreateAsync(products, productPaginationRequest);
        }

        #endregion Get
    }
}
