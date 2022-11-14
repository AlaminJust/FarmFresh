using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Dto.Response.Products;
using FarmFresh.Application.Interfaces.Services.Products;
using FarmFresh.Application.Models.Paginations;
using FarmFresh.Application.Models.Paginations.Products;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet("products")]
        [ProducesResponseType(typeof(PaginationResponse<ProductResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPaginatedProducts([FromQuery]ProductPaginationRequest productPaginationRequest)
        {
            var response = await _productService.GetPaginatedProductsAsync(productPaginationRequest);
            return Ok(response);
        }
        #endregion Get

        #region Save
        [Authorize(Roles = "Admin")]
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
