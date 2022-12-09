using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Interfaces.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace FarmFresh.Api.Controllers.Products
{
    [Route("api/discount-management")]
    [ApiController]
    public class DiscountController : ApiControllerBase
    {
        #region Properties
        private readonly IDiscountService _discountService;
        #endregion Properties

        #region Constructor
        public DiscountController(
                IDiscountService discountService
            )
        {
            _discountService = discountService;
        }
        #endregion Constructor

        #region Save
        [HttpPost("discount")]
        public async Task<IActionResult> AddAsync(DiscountRequest discountRequest)
        {
            var discountResponse = await _discountService.AddAsync(discountRequest);
            return Ok(discountResponse);
        }
        #endregion Save
    }
}
