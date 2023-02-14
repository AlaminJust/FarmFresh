using AutoMapper;
using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Dto.Response.Products;
using FarmFresh.Application.Interfaces.Services.Products;
using FarmFresh.Domain.Entities.Products;
using FarmFresh.Domain.RepoInterfaces.Products;

namespace FarmFresh.Infrastructure.Service.Services.Products
{
    public class CartItemService : ICartItemService
    {
        #region Properties
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        #endregion Properties

        #region Constructor
        public CartItemService(
                ICartItemRepository cartItemRepository,
                IMapper mapper,
                IProductService productService
            )
        {
            _cartItemRepository = cartItemRepository;
            _mapper = mapper;
            _productService = productService;
        }
        #endregion Constructor

        #region Save
        public async Task<CartItemResponse> AddAsync(CartItemRequest cartItem, int userId)
        {
            if (!await _productService.IsAvailableInStockAsync(cartItem.ProductId, cartItem.Quantity))
            {
                throw new Exception("Product is not available in stock");
            }
            
            CartItem cart = _mapper.Map<CartItem>(cartItem);


            await _cartItemRepository.AddAsync(cart);
            await _cartItemRepository.SaveChangesAsync();
            
            return _mapper.Map<CartItemResponse>(cart);
        }

        #endregion Save

        #region Delete
        public async Task RemoveCartItemAsync(int id)
        {
            var cartItem = await _cartItemRepository.GetByIdAsync(id);
            await _cartItemRepository.DeleteAsync(cartItem);
            await _cartItemRepository.SaveChangesAsync();
        }

        public async Task<Boolean> ClearUnavailableCartItem(ICollection<CartItemResponse> cartItems)
        {
            bool isAllAvailable = true;

            foreach (var item in cartItems)
            {
                if (!await _productService.IsAvailableInStockAsync(item.ProductId, item.Quantity))
                {
                    await this.RemoveCartItemAsync(item.Id);
                    isAllAvailable = false;
                }
            }

            return isAllAvailable;
        }

        #endregion Delete
    }
}
