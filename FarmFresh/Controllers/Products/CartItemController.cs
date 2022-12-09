using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Interfaces.Services.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FarmFresh.Api.Controllers.Products
{
    [Route("api/cart-item-management")]
    [ApiController]
    public class CartItemController : ApiControllerBase
    {
        #region Properties
        private readonly ICartItemService _cartItemService;
        #endregion Properties

        #region Constructor
        public CartItemController(
                ICartItemService cartItemService
            )
        {
            _cartItemService = cartItemService;
        }
        #endregion Constructor

        #region Save
        [HttpPost]
        [Route("cart-item")]
        [Authorize]
        public async Task<IActionResult> AddAsync([FromBody] CartItemRequest cartItemRequest)
        {
            var cartItemResponse = await _cartItemService.AddAsync(cartItemRequest, UserId);
            return Ok(cartItemResponse);
        }
        #endregion Save
    }
}
