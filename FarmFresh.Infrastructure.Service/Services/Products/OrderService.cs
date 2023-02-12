using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Interfaces.Services.Products;
using FarmFresh.Domain.Entities.Products;
using FarmFresh.Domain.RepoInterfaces;
using FarmFresh.Domain.RepoInterfaces.Products;
using Microsoft.EntityFrameworkCore.Storage;

namespace FarmFresh.Infrastructure.Service.Services.Products
{
    public class OrderService : IOrderService
    {
        private readonly ICartService _cartService;
        private readonly ITransactionUtil _transactionUtil;
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IDiscountRepository _discountRepository;

        public OrderService(
                ICartService cartService,
                ITransactionUtil transactionUtil,
                IOrderRepository orderRepository,
                IPaymentRepository paymentRepository,
                IDiscountRepository discountRepository
            )
        {
            _cartService = cartService;
            _transactionUtil = transactionUtil;
            _orderRepository = orderRepository;
            _paymentRepository = paymentRepository;
            _discountRepository = discountRepository;
        }
        public async Task OrderAsync(OrderRequest orderRequest)
        {
            var cart = await _cartService.GetCartByUserIdAsync(orderRequest.userId);
            await _transactionUtil.BeginAsync();

            try
            {
                var order = new Order
                {
                    UserId = orderRequest.userId,
                    OrderDate = DateTime.Now,
                    TotalAmount = cart.TotalPrice,
                    DiscountAmount = cart.DiscountPrice,
                    NetAmount = cart.FinalPrice,
                    PaymentStatus = orderRequest.paymentStatus,
                    OrderStatus = Application.Enums.OrderStatus.Processing
                };

                await _orderRepository.AddAsync(order);

                var paymentDetails = new PaymentDetail
                {
                    OrderId = order.Id,
                    TransactionId = orderRequest.transactionId,
                    PaymentMethod = orderRequest.paymentMethod,
                    PaymentDate = DateTime.Now,
                    Amount = orderRequest.Amount,
                    VoucherId = cart.Voucher?.Id
                };

                await _paymentRepository.AddAsync(paymentDetails);

                foreach(var item in cart.CartItems)
                {
                    Discount? discount = await _discountRepository.GetByIdAsync(item.productResponse.DiscountId ?? 0);

                    var orderItem = new OrderItem
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        Price = item.productResponse.Price,
                        Discount = item.productResponse.Discount,
                        Quantity = item.Quantity,
                        CreatedOn = DateTime.Now,
                        Total = ( item.productResponse.Price - discount?.DiscountValue ) * item.Quantity 
                    };
                    
                    
                }

            }
            catch (Exception)
            {
                await _transactionUtil.RollBackAsync();
                throw;
            }
        }
    }
}
