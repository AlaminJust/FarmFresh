using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Dto.Response.Products;
using Microsoft.AspNetCore.Http;

namespace FarmFresh.Application.Interfaces.Services.Products
{
    public interface IProductCategoryService
    {
        #region Properties
        string FolderPathUrl { get; }
        string ProductCategoryKey { get; }
        
        #endregion Properties

        #region Save
        Task<ProductCategoryResponse> AddAsync(ProductCategoryRequest productCategoryRequest);
        Task<ProductCategoryResponse> UpdateCategoryIconAsync(IFormFile file, int id);
        #endregion Save

        #region Get
        Task<IEnumerable<ProductCategoryResponse>> GetCategoriesTree();
        #endregion Get
    }
}
