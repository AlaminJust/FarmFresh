using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Dto.Response.Products;

namespace FarmFresh.Application.Interfaces.Services.Products
{
    public interface ICartService
    {
        #region Get
        Task<CartResponse> GetCartByIdAsync(int cartId);
        Task<CartResponse> GetCartByUserIdAsync(int userId);
        #endregion Get

        #region Save
        Task<CartResponse> AddToCartAsync(CartItemRequest cartItemRequest, int userId);
        #endregion Save

        #region Update
        Task<CartResponse> UpdateCartItemAsync(CartItemRequest cartItemRequest, int userId);
        #endregion Update
        #region Delete
        Task ClearCartAsync(int userId);
        #endregion Delete
    }
}
