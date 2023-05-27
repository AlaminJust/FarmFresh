using FarmFresh.Domain.Entities.Products;

namespace FarmFresh.Domain.RepoInterfaces.Products
{
    public interface ICartItemRepository: IBaseRepository<CartItem>
    {
        #region Get
        Task<IList<CartItem>> GetCartItemsByCartIdAsync(int cartId);
        #endregion Get

        #region Save
        Task<CartItem> AddCartItemAsync(CartItem cartItem);
        #endregion Save

        #region Update
        Task<CartItem> UpdateCartItemAsync(CartItem cartItem);
        #endregion Update
    }
}
