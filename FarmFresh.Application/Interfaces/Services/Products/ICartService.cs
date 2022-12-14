using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Dto.Response.Products;

namespace FarmFresh.Application.Interfaces.Services.Products
{
    public interface ICartService
    {
        #region Get
        Task<CartResponse> GetCartByIdAsync(int userId);
        #endregion Get

        #region Save
        Task<CartResponse> AddToCartAsync(CartItemRequest cartItemRequest, int userId);
        #endregion Save
    }
}
