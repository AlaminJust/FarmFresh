using FarmFresh.Application.Models.Paginations;
using FarmFresh.Application.Models.Paginations.Products;
using FarmFresh.Domain.Entities.Products;
using FarmFresh.Domain.ResponseEntities.Products;

namespace FarmFresh.Domain.RepoInterfaces.Products
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        #region Get
        Task<PaginationResponse<Product>> GetPaginatedProductsAsync(ProductPaginationRequest productPaginationRequest);
        Task<ProductDetails> GetProductDetailsByIdAsync(int id);
        Task<bool> IsAvailableInStockAsync(int productId, int quantity);
        Task<IEnumerable<AutoCompleteTrieSearchProduct>> AutoCompleteTrieSearchProductsAsync();
        #endregion Get

        #region Update
        Task UpdateProductStockAsync(int productId, int quantity);
        #endregion Update
    }
}
