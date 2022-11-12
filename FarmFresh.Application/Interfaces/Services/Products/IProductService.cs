using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Dto.Response.Products;
using FarmFresh.Domain.Entities.Products;

namespace FarmFresh.Application.Interfaces.Services.Products
{
    public interface IProductService
    {
        #region Save
        Task<ProductResponse> AddAsync(ProductRequest productRequest);
        #endregion Save
    }
}
