using FarmFresh.Domain.Entities.Products;
using FarmFresh.Domain.RepoInterfaces.Products;
using FarmFresh.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace FarmFresh.Infrastructure.Repo.Repositories.Products
{
    public class CartItemRepository : BaseRepository<CartItem>, ICartItemRepository
    {
        #region Properties
        private readonly EFDbContext _context;
        private readonly IProductRepository _productRepository;
        #endregion Properties

        #region Constructor
        public CartItemRepository(
                EFDbContext context,
                IProductRepository productRepository
            ) : base(context)
        {
            _context = context;
            _productRepository = productRepository;
        }
        #endregion Constructor

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
            var currentCartItem = await CartItems().Where(x => x.CartId == cartItem.CartId && x.ProductId == cartItem.ProductId).FirstOrDefaultAsync();
            
            if (currentCartItem is not null)
            {
                if(!await _productRepository.IsAvailableInStockAsync(cartItem.ProductId, currentCartItem.Quantity + cartItem.Quantity))
                {
                    throw new Exception("Product is not available in stock");
                }

                currentCartItem.Quantity += cartItem.Quantity;
                
                if(currentCartItem.Quantity <= 0)
                {
                    await this.DeleteAsync(currentCartItem);
                }
                else
                {
                    await UpdateAsync(currentCartItem);
                }
                
                await SaveChangesAsync();

                return currentCartItem;
            }
            else
            {
                if(cartItem.Quantity > 0)
                {
                    await AddAsync(cartItem);
                    await SaveChangesAsync();
                    return cartItem;
                }
                else
                {
                    throw new Exception("Quantity can't be negative.");
                }
            }
        }
        #endregion Save
    }
}
