using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Dto.Response.Products;

namespace FarmFresh.Application.Interfaces.Services.Products
{
    public interface IProductCategoryService
    {
        #region Save
        Task<ProductCategoryResponse> AddAsync(ProductCategoryRequest productCategoryRequest);
        #endregion Save
    }
}
