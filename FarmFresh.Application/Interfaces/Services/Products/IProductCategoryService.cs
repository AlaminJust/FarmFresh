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

        #region Get
        Task<IEnumerable<ProductCategoryResponse>> GetCategoriesTree();
        #endregion Get
        
        #region Save
        Task<ProductCategoryResponse> AddAsync(ProductCategoryRequest productCategoryRequest);
        Task<ProductCategoryResponse> UpdateCategoryIconAsync(IFormFile file, int id);
        #endregion Save

        #region Update
        Task<ProductCategoryResponse> UpdateAsync(ProductCategoryUpdateRequest productCategoryRequest, int id, int userId);

        #endregion Update
    }
}
