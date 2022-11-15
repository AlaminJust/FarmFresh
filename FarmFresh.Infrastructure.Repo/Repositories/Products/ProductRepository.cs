using FarmFresh.Application.Models.Paginations;
using FarmFresh.Application.Models.Paginations.Products;
using FarmFresh.Domain.Entities.Products;
using FarmFresh.Domain.ResponseEntities.Products;
using FarmFresh.Domain.RepoInterfaces.Products;
using FarmFresh.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

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

        public async Task<ProductDetails> GetProductDetailsByIdAsync(int id)
        {
            var productDetails = (from product in _context.Products
                                 join brand in _context.Brands on product.BrandId equals brand.Id
                                 join category in _context.ProductCategories on product.CategoryId equals category.Id
                                 join vendor in _context.Vendors on product.VendorId equals vendor.Id
                                 join user1 in _context.Users on product.CreatedBy equals user1.Id
                                 join user2 in _context.Users on product.UpdatedBy equals user2.Id
                                 where (product.Id == id)
                                 select new ProductDetails
                                 {
                                     Name = product.Name,
                                     Description = product.Description,
                                     Price = product.Price,
                                     Quantity = product.Quantity,
                                     BrandId = product.BrandId,
                                     CategoryId = product.CategoryId,
                                     VendorId = product.VendorId,
                                     BrandName = brand.Name,
                                     CategoryName = category.CategoryName,
                                     VendorName = vendor.Name,
                                     CreatedBy = user1.Id,
                                     CreatedByName = user1.UserName,
                                     UpdatedBy = user2.Id,
                                     UpdatedByName = user2.UserName,
                                     CreatedDate = product.CreatedOn,
                                     OldPrice = product.OldPrice,
                                     ImageUrls = product.ImageUrls,
                                     UpdatedDate = product.UpdatedOn,
                                 }).FirstOrDefaultAsync();



            return await productDetails;

        }

        #endregion Get
    }
}
