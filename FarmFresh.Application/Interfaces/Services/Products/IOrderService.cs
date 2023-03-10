using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Dto.Response.Products;
using System.Runtime.CompilerServices;

namespace FarmFresh.Application.Interfaces.Services.Products
{
    public interface IOrderService
    {
        #region Save
        Task<Int32> OrderAsync(OrderRequest orderRequest, int userId);
        #endregion Save

        #region Get
        Task<List<OrderResponse>> GetOrdersByUserIdAsync(int userId);
        Task<List<OrderResponse>> GetOrdersOfAllUsersAsync();
        #endregion Get
    }
}
