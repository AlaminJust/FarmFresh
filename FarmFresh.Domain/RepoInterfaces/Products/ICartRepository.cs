using FarmFresh.Domain.Entities.Products;

namespace FarmFresh.Domain.RepoInterfaces.Products
{
    public interface ICartRepository : IBaseRepository<Cart>
    {
        #region Get
        Task<Cart> GetCartByUserIdAsync(int userId);
        #endregion Get

        #region Save
        Task<Cart> CreateCartAsync(int userId);
        #endregion Save
    }
}
