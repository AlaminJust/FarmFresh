using AutoMapper;
using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Dto.Response.Products;
using FarmFresh.Application.Enums;
using FarmFresh.Application.Extensions;
using FarmFresh.Application.Interfaces.Services.Products;
using FarmFresh.Application.Interfaces.Services.Users;
using FarmFresh.Domain.Entities.Products;
using FarmFresh.Domain.RepoInterfaces;
using FarmFresh.Domain.RepoInterfaces.Products;
using System.Linq;

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
        private readonly IProductHistoryService _productHistoryService;
        private readonly IProductRepository _productRepository;
        private readonly IRoleService _roleService;
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
                IProductHistoryService productHistoryService,
                IProductRepository productRepository,
                IRoleService roleService,
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
            _productHistoryService = productHistoryService;
            this._productRepository = productRepository;
            this._roleService = roleService;
            _mapper = mapper;
        }

        #region Private Method

        private async Task BackItemQuantityWhenOrderCacelledOrRefundAsync(Order order)
        {
            var orderItem = order.OrderItems;

            foreach(var item in orderItem)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                product.Quantity += item.Quantity;
                await _productRepository.UpdateAsync(product);
                await _productRepository.SaveChangesAsync();
            }
        }

        #endregion Private Method

        #region Save
        public async Task<int> OrderAsync(OrderRequest orderRequest, int userId)
        {
            // Retrieve the cart for the user
            var cart = await _cartService.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                throw new Exception("Your cart is empty!");
            }

            // Check if the cart total matches the order amount
            if (cart.FinalPrice != orderRequest.Amount)
            {
                throw new Exception("The order failed because the total amount is not correct!");
            }

            // Clear any cart items that are unavailable
            if (!await _cartItemService.ClearUnavailableCartItem(cart.CartItems))
            {
                throw new Exception("The order failed because some items are not available now. We have cleared these items from your cart!");
            }
            
            await _transactionUtil.BeginAsync();

            try
            {
                // Create a new order object
                var order = new Order
                {
                    UserId = userId,
                    OrderDate = DateTime.UtcNow,
                    TotalAmount = cart.TotalPrice,
                    DiscountAmount = cart.DiscountPrice,
                    NetAmount = cart.FinalPrice,
                    Address = orderRequest.Address,
                    PaymentStatus = orderRequest.paymentStatus,
                    OrderStatus = Application.Enums.OrderStatus.Processing
                };

                // Add the order to the repository and save changes
                await _orderRepository.AddAsync(order);
                await _orderRepository.SaveChangesAsync();

                // Create a new payment details object
                var paymentDetails = new PaymentDetail
                {
                    OrderId = order.Id,
                    TransactionId = orderRequest.transactionId,
                    PaymentMethod = orderRequest.paymentMethod,
                    PaymentDate = DateTime.UtcNow,
                    Amount = cart.FinalPrice,
                    VoucherId = cart.Voucher?.Id,
                    VoucherDiscount = cart.Voucher?.Discount
                };

                // Add the payment details to the repository and save changes
                await _paymentRepository.AddAsync(paymentDetails);
                await _paymentRepository.SaveChangesAsync();

                // Create order items for each item in the cart
                foreach (var item in cart.CartItems)
                {
                    var discount = await _discountService.GetDiscountByIdAsync(item.productResponse.DiscountId ?? 0);

                    var orderItem = new OrderItem
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        Price = item.productResponse.Price,
                        Discount = item.productResponse.Discount,
                        Quantity = item.Quantity,
                        CreatedOn = DateTime.UtcNow,
                        Product = null,
                        Total = item.productResponse.CalculatePriceWithDiscount(discount)
                    };

                    // Add the order item to the repository and save changes
                    await _orderItemRepository.AddAsync(orderItem);
                    await _orderItemRepository.SaveChangesAsync();

                    // Update product history to keep track the specific product data
                    var productHistory = new ProductHistoryRequest
                    {
                        ProductId = item.ProductId,
                        Point = item.Quantity,
                        UpdateBy = userId,
                        HistoryType = ProductHistoryType.Buy
                    };

                    await _productHistoryService.AddAsync(productHistory);

                    // Update the product stock
                    await _productService.UpdateProductStockAsync(item.ProductId, -item.Quantity);
                }

                // Clear the user's cart
                await _cartService.ClearCartAsync(userId);

                // Commit the transaction
                await _transactionUtil.CommitAsync();

                // Return the order ID
                return order.Id;
            }
            catch (Exception)
            {
                // Roll back the transaction and rethrow the exception
                await _transactionUtil.RollBackAsync();
                throw;
            }
        }
        public async Task SaveStatusAsync(int orderId, OrderStatus request, int userId)
        {
            var userRole = await _roleService.GetRoleNamesByUserIdAsync(userId);
            bool isAdmin = userRole.Any(role => string.Equals(role, "admin", StringComparison.InvariantCultureIgnoreCase));

            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order == null)
            {
                throw new Exception("Your order has not found!");
            }
            if (order.OrderStatus == OrderStatus.Cancelled)
            {
                throw new Exception("Your order already has been cancelled.");
            }
            else if (order.UserId != userId && !isAdmin)
            {
                throw new UnauthorizedAccessException();
            }
            else if(order.UserId == userId && !isAdmin && order.OrderStatus != OrderStatus.Processing)
            {
                throw new Exception("Sorry your order is already verified, You can't able to change the status now. Please contact to our customer care.");
            }

            await _transactionUtil.BeginAsync();

            try
            {
                if(order.OrderStatus != request)
                {
                    if (OrderStatus.Cancelled == request || OrderStatus.Returned == request)
                    {
                        await BackItemQuantityWhenOrderCacelledOrRefundAsync(order);
                    }

                    order.OrderStatus = request;
                    await _orderRepository.SaveChangesAsync();
                }

                await _transactionUtil.CommitAsync();
            }
            catch (Exception)
            {
                await _transactionUtil.RollBackAsync();
                throw;
            }
        }

        public async Task SavePaymentStatusAsync(int orderId, PaymentStatus request)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order == null)
            {
                throw new Exception("Order not found!");
            }

            order.PaymentStatus = request;
            await _orderRepository.SaveChangesAsync();
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