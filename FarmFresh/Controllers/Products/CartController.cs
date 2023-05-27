using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Dto.Response.Products;
using FarmFresh.Application.Interfaces.Services.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FarmFresh.Api.Controllers.Products
{
    [Route("api/cart-management")]
    [ApiController]
    public class CartController : ApiControllerBase
    {
        #region Properties
        private readonly ICartService _cartService;
        #endregion Properties

        #region Constructor
        public CartController(
                ICartService cartService
            )
        {
            _cartService = cartService;
        }
        #endregion Constructor

        #region Save
        [HttpPost]
        [Route("cart-item")]
        [Authorize]
        [ProducesResponseType(typeof(CartResponse), 200)]
        public async Task<IActionResult> AddAsync([FromBody] CartItemRequest cartItemRequest)
        {
            CartResponse response = await _cartService.AddToCartAsync(cartItemRequest, UserId);
            return Ok(response);
        }

        #endregion Save

        #region Update
        [HttpPut]
        [Route("cart-item")]
        [Authorize]
        [ProducesResponseType(typeof(CartResponse), 200)]
        public async Task<IActionResult> UpdateAsync([FromBody] CartItemRequest cartItemRequest)
        {
            CartResponse response = await _cartService.UpdateCartItemAsync(cartItemRequest, UserId);
            return Ok(response);
        }
        #endregion Update

        #region Get
        [HttpGet]
        [Route("cart-item")]
        [Authorize]
        [ProducesResponseType(typeof(CartResponse), 200)]
        public async Task<IActionResult> GetAsync()
        {
            CartResponse response = await _cartService.GetCartByUserIdAsync(UserId);
            return Ok(response);
        }
        #endregion Get
    }
}
