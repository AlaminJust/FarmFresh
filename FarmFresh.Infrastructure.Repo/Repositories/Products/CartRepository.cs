using FarmFresh.Domain.Entities.Products;
using FarmFresh.Domain.RepoInterfaces.Products;
using FarmFresh.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace FarmFresh.Infrastructure.Repo.Repositories.Products
{
    public class CartRepository : BaseRepository<Cart>, ICartRepository
    {
        #region Properties
        private readonly EFDbContext _context;
        #endregion Properties

        #region Constructor
        public CartRepository(EFDbContext context) : base(context)
        {
            _context = context;
        }
        #endregion Constructor

        #region Get
        public async Task<Cart> GetCartByUserIdAsync(int userId)
        {
            return await _context.Carts.FirstOrDefaultAsync(x => x.UserId == userId);
        }
        #endregion Get

        #region Save
        public async Task<Cart> CreateCartAsync(int userId)
        {
            /* TODO: Checking user already have a cart or not
               TODO: If not then create a new cart
               TODO: If yes then return the existing cart */

            Cart cart = await GetCartByUserIdAsync(userId);
            
            if (cart is null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    TotalPrice = 0,
                    CreatedOn = DateTime.UtcNow
                };
                await AddAsync(cart);
                await SaveChangesAsync();
            }

            return cart;
        }
        #endregion Save

    }
}
