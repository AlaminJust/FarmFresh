using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Dto.Response.Products;
using FarmFresh.Application.Interfaces.Services.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FarmFresh.Api.Controllers.Products
{
    [Route("api/product-management")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        #region Properties
        private readonly IProductService _productService;
        #endregion Properties

        #region Constructor
        public ProductController(
                IProductService productService
            )
        {
            _productService = productService;
        }
        #endregion Constructor

        #region Get 

        #endregion Get

        #region Save
        [HttpPost("product")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddProduct([FromBody] ProductRequest productRequest)
        {
            var response = await _productService.AddAsync(productRequest);
            return Ok(response);
        }
        #endregion Save
    }
}
