using AutoMapper;
using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Dto.Response.Products;
using FarmFresh.Application.Extensions;
using FarmFresh.Application.Interfaces.Services.Products;
using FarmFresh.Application.Interfaces.Services.Users;
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
        private readonly IMapper _mapper;

        public OrderService(
                ICartService cartService,
                ITransactionUtil transactionUtil,
                IOrderRepository orderRepository,
                IPaymentRepository paymentRepository,
                IDiscountService discountService,
                IOrderItemRepository orderItemRepository,
                IProductService productService,
                ICartItemService cartItemService,
                IMapper mapper
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
            _mapper = mapper;
        }
        #region Save
        public async Task<Int32> OrderAsync(OrderRequest orderRequest, int userId)
        {
            var cart = await _cartService.GetCartByUserIdAsync(userId);

            if (cart is null)
            {
                throw new Exception("Your cart is empty!");
            }

            if (cart.FinalPrice != orderRequest.Amount)
            {
                throw new Exception("The order failed becouse the total amount is not correct!");
            }

            if (cart is not null && !await _cartItemService.ClearUnavailableCartItem(cart.CartItems))
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
                    Address = orderRequest.Address,
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
                    Amount = cart.FinalPrice,
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
        #endregion Save

        #region Get
        public async Task<List<OrderResponse>> GetOrdersByUserIdAsync(int userId)
        {
            var orderDetails = await _orderRepository.GetOrdersByUserIdAsync(userId);
            return _mapper.Map<List<OrderResponse>>(orderDetails);
        }

        public async Task<List<OrderResponse>> GetOrdersOfAllUsersAsync()
        {
            var orderDetails = await _orderRepository.GetOrdersOfAllUsersAsync();
            return _mapper.Map<List<OrderResponse>>(orderDetails);
        }
        #endregion Get
    }
}