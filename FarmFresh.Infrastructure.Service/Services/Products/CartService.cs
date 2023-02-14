using AutoMapper;
using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Dto.Response.Products;
using FarmFresh.Application.Interfaces.Services.Products;
using FarmFresh.Domain.Entities.Products;
using FarmFresh.Domain.Entities.Users;
using FarmFresh.Domain.RepoInterfaces;
using FarmFresh.Domain.RepoInterfaces.Products;

namespace FarmFresh.Infrastructure.Service.Services.Products
{
    public class CartService : ICartService
    {
        #region Properties
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly ICartItemService _cartItemService;
        private readonly IProductService _productService;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IVoucherRepository _voucherRepository;
        private readonly ITransactionUtil _transactionUtil;
        #endregion Properties

        #region Constructor

        public CartService(
                ICartRepository cartRepository,
                IMapper mapper,
                ICartItemService cartItemService,
                IProductService productService,
                ICartItemRepository cartItemRepository,
                IProductRepository productRepository,
                IDiscountRepository discountRepository,
                IVoucherRepository voucherRepository,
                ITransactionUtil transactionUtil
            )
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _cartItemService = cartItemService;
            _productService = productService;
            _cartItemRepository = cartItemRepository;
            _productRepository = productRepository;
            _discountRepository = discountRepository;
            _voucherRepository = voucherRepository;
            _transactionUtil = transactionUtil;
        }
        #endregion Constructor

        #region Private method

        private async Task<decimal> CalculateTotalPriceAsync(ICollection<CartItem> cartItems)
        {
            decimal totalPrice = 0;

            foreach (var item in cartItems)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                totalPrice += product.Price * item.Quantity;
            }

            return totalPrice;
        }

        private async Task<decimal> CalculateDiscountAsync(ICollection<CartItem> cartItems)
        {
            decimal totalDiscount = 0;

            foreach (var item in cartItems)
            {
                decimal discount = await _discountRepository.CalculateDiscountOfProductAsync(item.ProductId);
                totalDiscount += discount * item.Quantity;
            }

            return totalDiscount;
        }

        private async Task UpdateCartPriceAsync(Cart cart)
        {
            if (cart is null)
            {
                throw new ArgumentNullException(nameof(cart));
            }

            cart.CartItems = await _cartItemRepository.GetCartItemsByCartIdAsync(cart.Id);
            decimal totalPrice = await CalculateTotalPriceAsync(cart.CartItems);
            decimal discountPrice = await CalculateDiscountAsync(cart.CartItems);
            decimal voucherDiscount = await _voucherRepository.ApplyVoucherAsync(cart.VoucherId ?? 0, totalPrice - discountPrice);

            cart.TotalPrice = totalPrice;
            cart.DiscountPrice = (discountPrice + voucherDiscount);
            cart.UpdatedOn = DateTime.Now;
            
            await _cartRepository.UpdateAsync(cart);
            await _cartItemRepository.SaveChangesAsync();
        }

        #endregion Private method

        #region Get
        public async Task<CartResponse> GetCartByIdAsync(int cartId)
        {
            var cart = await _cartRepository.GetByIdAsync(cartId);
            Dictionary<int, Product> storeProduct = new();

            if (cart is not null)
            {
                cart.CartItems = await _cartItemRepository.GetCartItemsByCartIdAsync(cart.Id);

                foreach (var item in cart.CartItems)
                {
                    item.Product = await _productRepository.GetByIdAsync(item.ProductId);
                    storeProduct.TryAdd(item.ProductId, item.Product);
                }

                cart.Voucher = await _voucherRepository.GetByIdAsync(cart.VoucherId ?? 0);
            }

            var response = _mapper.Map<CartResponse>(cart);

            foreach (var item in response.CartItems)
            {
                item.productResponse = _mapper.Map<ProductResponse>(storeProduct[item.ProductId]);
            }

            return response;
        }

        #endregion Get

        #region Save
        public async Task<CartResponse> AddToCartAsync(CartItemRequest cartItemRequest, int userId)
        {
            if (!await _productService.IsAvailableInStockAsync(cartItemRequest.ProductId, cartItemRequest.Quantity))
            {
                throw new Exception("Product is not available in stock");
            }
            
            Cart cart = new();
            
            await _transactionUtil.BeginAsync();
            
            try
            {
                cart = await _cartRepository.CreateCartAsync(userId);
                CartItem cartItem = _mapper.Map<CartItem>(cartItemRequest);
                cartItem.CartId = cart.Id;
                cartItem.CreatedOn = DateTime.Now;

                await _cartItemRepository.AddCartItemAsync(cartItem);

                await UpdateCartPriceAsync(cart);

                await _transactionUtil.CommitAsync();
            }
            catch (Exception)
            {
                await _transactionUtil.RollBackAsync();
                throw;
            }

            return await GetCartByIdAsync(cart.Id);

        }
        #endregion Save

        #region Get
        public async Task<CartResponse> GetCartByUserIdAsync(int userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            return await GetCartByIdAsync(cart.Id);
        }

        #endregion Get

        #region Delete
        public async Task ClearCartAsync(int userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            await _cartRepository.DeleteAsync(cart);
            await _cartRepository.SaveChangesAsync();
        }
        #endregion Delete
    }
}
