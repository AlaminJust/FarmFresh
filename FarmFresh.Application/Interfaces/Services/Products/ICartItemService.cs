using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Dto.Response.Products;

namespace FarmFresh.Application.Interfaces.Services.Products
{
    public interface ICartItemService
    {
        #region Save
        Task<CartItemResponse> AddAsync(CartItemRequest cartItem, int userId);
        #endregion Save

        #region Delete
        Task RemoveCartItemAsync(int id);
        public Task<Boolean> ClearUnavailableCartItem(ICollection<CartItemResponse> cartItems);
        #endregion Delete
    }
}
