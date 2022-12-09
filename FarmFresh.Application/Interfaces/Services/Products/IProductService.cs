using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Dto.Response.Products;
using FarmFresh.Application.Models.Paginations;
using FarmFresh.Application.Models.Paginations.Products;

namespace FarmFresh.Application.Interfaces.Services.Products
{
    public interface IProductService
    {
        #region Get
        Task<PaginationResponse<ProductResponse>> GetPaginatedProductsAsync(ProductPaginationRequest productPaginationRequest);
        Task<Boolean> IsAvailableInStockAsync(int productId, int quantity);
        #endregion Get

        #region Save
        Task<ProductResponse> AddAsync(ProductRequest productRequest, int userId);
        Task<ProductDetailsResponse> GetProductDetailsByIdAsync(int id);
        #endregion Save
    }
}
