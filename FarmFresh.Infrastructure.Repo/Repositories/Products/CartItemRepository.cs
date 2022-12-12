using FarmFresh.Domain.Entities.Products;
using FarmFresh.Domain.RepoInterfaces.Products;
using FarmFresh.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace FarmFresh.Infrastructure.Repo.Repositories.Products
{
    public class CartItemRepository : BaseRepository<CartItem>, ICartItemRepository
    {
        private readonly EFDbContext _context;

        public CartItemRepository(EFDbContext context) : base(context)
        {
            _context = context;
        }

        #region Private Method
        private IQueryable<CartItem> CartItems()
        {
            return _context.CartItems.Where(x => x.IsDeleted == false);
        }
        #endregion Private Method

        #region Get
        public async Task<IList<CartItem>> GetCartItemsByCartIdAsync(int cartId)
        {
            return await CartItems().Where(x => x.CartId == cartId).ToListAsync();
        }

        #endregion Get

        #region Save
        public async Task<CartItem> AddCartItemAsync(CartItem cartItem)
        {
            var existingCartItem = await CartItems().Where(x => x.CartId == cartItem.CartId && x.ProductId == cartItem.ProductId).FirstOrDefaultAsync();
            
            if (existingCartItem is not null)
            {
                existingCartItem.Quantity += cartItem.Quantity;
                await UpdateAsync(existingCartItem);
                await SaveChangesAsync();
                return existingCartItem;
            }
            else
            {
                await AddAsync(cartItem);
                await SaveChangesAsync();
                return cartItem;
            }
        }
        #endregion Save
    }
}
