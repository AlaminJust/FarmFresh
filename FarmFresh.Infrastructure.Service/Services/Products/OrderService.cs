using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Dto.Response.Products;
using FarmFresh.Application.Extensions;
using FarmFresh.Application.Interfaces.Services.Products;
using FarmFresh.Domain.Entities.Products;
using FarmFresh.Domain.RepoInterfaces;
using FarmFresh.Domain.RepoInterfaces.Products;

namespace FarmFresh.Infrastructure.Service.Services.Products
{
    public class OrderService : IOrderService
    {
        private readonly ICartService _cartService;
        private readonly ITransactionUtil _transactionUtil;
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IDiscountService _discountService;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IProductService _productService;
        private readonly ICartItemService _cartItemService;

        public OrderService(
                ICartService cartService,
                ITransactionUtil transactionUtil,
                IOrderRepository orderRepository,
                IPaymentRepository paymentRepository,
                IDiscountService discountService,
                IOrderItemRepository orderItemRepository,
                IProductService productService,
                ICartItemService cartItemService
            )
        {
            _cartService = cartService;
            _transactionUtil = transactionUtil;
            _orderRepository = orderRepository;
            _paymentRepository = paymentRepository;
            _discountService = discountService;
            _orderItemRepository = orderItemRepository;
            _productService = productService;
            _cartItemService = cartItemService;
        }

        private async Task<Boolean> ClearUnavailableCartItem(ICollection<CartItemResponse> cartItems)
        {
            bool isAllAvailable = true;
            
            foreach(var item in cartItems)
            {
                if (!await _productService.IsAvailableInStockAsync(item.ProductId, item.Quantity))
                {
                    await _cartItemService.RemoveCartItemAsync(item.Id);
                    isAllAvailable = false;
                }
            }
            
            return isAllAvailable;
        }


        public async Task<Int32> OrderAsync(OrderRequest orderRequest, int userId)
        {
            var cart = await _cartService.GetCartByUserIdAsync(userId);

            if (cart is null)
            {
                throw new Exception("Your cart is empty!");
            }

            if (cart is not null && !await ClearUnavailableCartItem(cart.CartItems))
            {
                throw new Exception("The order failed becouse some of the item is not available now. We have cleared these item from your cart!");
            }
            
            await _transactionUtil.BeginAsync();

            try
            {
                var order = new Order
                {
                    UserId = userId,
                    OrderDate = DateTime.Now,
                    TotalAmount = cart.TotalPrice,
                    DiscountAmount = cart.DiscountPrice,
                    NetAmount = cart.FinalPrice,
                    PaymentStatus = orderRequest.paymentStatus,
                    OrderStatus = Application.Enums.OrderStatus.Processing
                };

                await _orderRepository.AddAsync(order);
                await _orderRepository.SaveChangesAsync();

                var paymentDetails = new PaymentDetail
                {
                    OrderId = order.Id,
                    TransactionId = orderRequest.transactionId,
                    PaymentMethod = orderRequest.paymentMethod,
                    PaymentDate = DateTime.Now,
                    Amount = orderRequest.Amount,
                    VoucherId = cart.Voucher?.Id,
                    VoucherDiscount = cart.Voucher?.Discount
                };
                
                await _paymentRepository.AddAsync(paymentDetails);
                await _paymentRepository.SaveChangesAsync();

                
                foreach(var item in cart.CartItems)
                {
                    var discount = await _discountService.GetDiscountByIdAsync(item.productResponse.DiscountId ?? 0);
                    
                    var orderItem = new OrderItem
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        Price = item.productResponse.Price,
                        Discount = item.productResponse.Discount,
                        Quantity = item.Quantity,
                        CreatedOn = DateTime.Now,
                        Total = item.productResponse.CalculatePriceWithDiscount(discount) 
                    };
                    
                    await _orderItemRepository.AddAsync(orderItem);
                    await _orderItemRepository.SaveChangesAsync();

                    await _productService.UpdateProductStockAsync(item.ProductId, -item.Quantity);
                }

                await _cartService.ClearCartAsync(userId);
                
                await _transactionUtil.CommitAsync();

                return order.Id;
            }
            catch (Exception)
            {
                await _transactionUtil.RollBackAsync();
                throw;
            }
        }
    }
}