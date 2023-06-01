using FarmFresh.Application.Dto.Response.Products;
using FarmFresh.Application.Interfaces.Services.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FarmFresh.Api.Controllers.Products
{
    [Route("api/product-history-management")]
    [ApiController]
    public class ProductHistoryController : ApiControllerBase
    {
        #region Properties
        private readonly IProductHistoryService _productHistoryService;
        #endregion Properties

        #region Ctor
        public ProductHistoryController(
                IProductHistoryService productHistoryService
            )
        {
            _productHistoryService = productHistoryService;
        }
        #endregion Ctor

        #region Get
        
        [Authorize]
        [HttpGet("history/{productId}/{dateRange}")]
        [ProducesResponseType(typeof(IEnumerable<ProductHistoryResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductHistory([FromRoute] int productId, [FromRoute] int dateRange)
        {
            var response = await _productHistoryService.GetHistoryByDateRange(productId,dateRange);
            return Ok(response);
        }

        #endregion Get
    }
}
