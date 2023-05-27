using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Dto.Request.Users;
using FarmFresh.Application.Dto.Response.Products;
using FarmFresh.Application.Enums;
using FarmFresh.Application.Extensions;
using FarmFresh.Application.Interfaces.Services.Products;
using FarmFresh.Application.Interfaces.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FarmFresh.Api.Controllers.Products
{
    [Route("api/order-management")]
    [ApiController]
    [Authorize]
    public class OrderController : ApiControllerBase
    {
        #region Fields
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        #endregion Fields

        #region Constructor

        public OrderController(
                IOrderService orderService,
                IUserService userService
            )
        {
            _orderService = orderService;
            _userService = userService;
        }

        #endregion Constructor

        #region Save
        [HttpPost]
        [Route("order")]
        [ProducesResponseType(typeof(Int32), StatusCodes.Status200OK)]
        public async Task<IActionResult> Save([FromBody] OrderRequest request)
        {
            try
            {
                var order = await _orderService.OrderAsync(request, UserId);
                
                await _userService.UpdateAsync(UserId, new UserAddressRequest
                {
                    ShippingAddress = request.Address
                });
                
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("order/{orderId}/payment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SavePayment([FromRoute]int orderId, [FromBody] PaymentStatus request)
        {
            try
            {
                await _orderService.SavePaymentStatusAsync(orderId, request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("order/{orderId}/status")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> SaveStatus([FromRoute]int orderId, [FromBody] OrderStatus request)
        {
            try
            {
                await _orderService.SaveStatusAsync(orderId, request);

                var status = new
                {
                    message = $"Your order status has been changed successfully to {request.GetDescription()}"
                };
                
                return Ok(status);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion Save

        #region Get

        [HttpGet("orders")]

        [ProducesResponseType(typeof(List<OrderResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrders()
        {
            try
            {
                var orders = await _orderService.GetOrdersByUserIdAsync(UserId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("users-order")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(List<OrderResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrdersOfAllUsers()
        {
            try
            {
                var orders = await _orderService.GetOrdersOfAllUsersAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

    }
}