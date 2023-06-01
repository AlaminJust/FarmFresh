using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Dto.Response.Products;

namespace FarmFresh.Application.Interfaces.Services.Products;

public interface IProductHistoryService
{
    #region Get
    Task<ProductHistoryResponse> GetHistoryByDateRange(int productId, int dateRange, int column = 30);
    #endregion Get
    
    #region Save
    Task AddAsync(ProductHistoryRequest request, int updatedBy);

    #endregion Save
}