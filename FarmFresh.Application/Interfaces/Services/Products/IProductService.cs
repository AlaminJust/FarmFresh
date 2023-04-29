using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Dto.Response.Products;
using FarmFresh.Application.Models.Paginations;
using FarmFresh.Application.Models.Paginations.Products;
using Microsoft.AspNetCore.Http;

namespace FarmFresh.Application.Interfaces.Services.Products
{
    public interface IProductService
    {
        #region Properties
        public string ImageUrlPath { get; }
        #endregion Properties

        #region Get
        Task<PaginationResponse<ProductResponse>> GetPaginatedProductsAsync(ProductPaginationRequest productPaginationRequest);
        Task<Boolean> IsAvailableInStockAsync(int productId, int quantity);
        #endregion Get

        #region Save
        Task<ProductResponse> AddAsync(ProductRequest productRequest, int userId);
        Task<ProductDetailsResponse> GetProductDetailsByIdAsync(int id);
        Task<ProductResponse> UpdateProductImageAsync(IFormFile file, int id);
        #endregion Save

        #region Update
        Task UpdateProductStockAsync(int productId, int quantity);
        Task<ProductResponse> UpdateProductAsync(ProductUpdateRequest productUpdateRequest, int id, int updatedBy);
        #endregion Update
    }
}
