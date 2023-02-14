using FarmFresh.Application.Models.Paginations;
using FarmFresh.Application.Models.Paginations.Products;
using FarmFresh.Domain.Entities.Products;
using FarmFresh.Domain.ResponseEntities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmFresh.Domain.RepoInterfaces.Products
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        #region Get
        Task<PaginationResponse<Product>> GetPaginatedProductsAsync(ProductPaginationRequest productPaginationRequest);
        Task<ProductDetails> GetProductDetailsByIdAsync(int id);
        Task<bool> IsAvailableInStockAsync(int productId, int quantity);
        #endregion Get

        #region Update
        Task UpdateProductStockAsync(int productId, int quantity);
        #endregion Update
    }
}
