using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Dto.Response.Products;
using FarmFresh.Application.Interfaces.Services.Caches;
using FarmFresh.Application.Interfaces.Services.Products;
using FarmFresh.Application.Models.Paginations;
using FarmFresh.Application.Models.Paginations.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FarmFresh.Api.Controllers.Products
{
    [Route("api/product-management")]
    [ApiController]
    public class ProductController : ApiControllerBase
    {
        #region Properties
        private readonly IProductService _productService;
        private readonly ICacheService _cacheService;
        private readonly ISuggesionService _suggesionService;
        #endregion Properties

        #region Constructor
        public ProductController(
                IProductService productService,
                ICacheService cacheService,
                ISuggesionService suggesionService
            )
        {
            _productService = productService;
            _cacheService = cacheService;
            _suggesionService = suggesionService;
        }
        #endregion Constructor

        #region Get 
        [HttpGet("products")]
        [ProducesResponseType(typeof(PaginationResponse<ProductResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPaginatedProducts([FromQuery] ProductPaginationRequest productPaginationRequest)
        {
            var cacheKey = productPaginationRequest.GetCacheKey();

            var response = await _cacheService.GetDataAsync<PaginationResponse<ProductResponse>>(cacheKey);

            if (response == null)
            {
                response = await _productService.GetPaginatedProductsAsync(productPaginationRequest);
                await _cacheService.SetDataAsync(cacheKey, response, TimeSpan.FromDays(1));
            }

            return Ok(response);
        }

        [HttpGet("product/details/{Id}")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductDetailsById([FromRoute] int Id)
        {
            var response = await _productService.GetProductDetailsByIdAsync(Id);
            return Ok(response);
        }

        [HttpGet("product/suggestion")]
        [ProducesResponseType(typeof(List<AutoCompleteTrieSearchProductResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductSuggesion([FromQuery] string text)
        {
            var response = await _suggesionService.AutoCompleteTrieSearchProductResponse(text);
            return Ok(response);
        }

        #endregion Get

        #region Save
        [Authorize(Roles = "Admin")]
        [HttpPost("product")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddProduct([FromBody] ProductRequest productRequest)
        {
            var response = await _productService.AddAsync(productRequest, UserId);
            await _cacheService.RemoveByPrefixAsync(ProductPaginationRequestExtensions.PrefixKey);
            return Ok(response);
        }
        #endregion Save

        #region Update

        [Authorize(Roles = "Admin")]
        [HttpPut("product/image/{id}")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProductImage(IFormFile file, [FromRoute] int id)
        {
            var productCategory = await _productService.UpdateProductImageAsync(file, id);
            await _cacheService.RemoveByPrefixAsync(ProductPaginationRequestExtensions.PrefixKey);
            return Ok(productCategory);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("product/{id}")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateRequest productUpdateRequest, [FromRoute] int id)
        {
            var productCategory = await _productService.UpdateProductAsync(productUpdateRequest, id, UserId);
            await _cacheService.RemoveByPrefixAsync(ProductPaginationRequestExtensions.PrefixKey);
            return Ok(productCategory);
        }
        #endregion Update
    }
}
