using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Dto.Response.Products;
using FarmFresh.Application.Interfaces.Services.Caches;
using FarmFresh.Application.Interfaces.Services.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FarmFresh.Api.Controllers.Products
{
    [Route("api/product-category-management")]
    [ApiController]
    public class ProductCategoryController : ApiControllerBase
    {
        #region Properties
        private readonly IProductCategoryService _productCategoryService;
        private readonly ICacheService _cacheService;
        private readonly IWebHostEnvironment _environment;
        #endregion Properties

        #region Constructor

        public ProductCategoryController(
                IProductCategoryService productCategoryService,
                ICacheService cacheService,
                IWebHostEnvironment environment)
        {
            _productCategoryService = productCategoryService;
            _cacheService = cacheService;
            _environment = environment;
        }

        #endregion Constructor

        #region Get
        [HttpGet("categories")]
        [ProducesResponseType(typeof(IEnumerable<ProductCategoryResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductCategories()
        {
            var cacheKey = _productCategoryService.ProductCategoryKey;

            var response = await _cacheService.GetDataAsync<IEnumerable<ProductCategoryResponse>>(cacheKey);

            if (response == null)
            {
                response = await _productCategoryService.GetCategoriesTree();
                await _cacheService.SetDataAsync(cacheKey, response, TimeSpan.FromDays(1));
            }

            return Ok(response);
        }

        #endregion Get

        #region Save
        [Authorize(Roles = "Admin")]
        [HttpPost("category")]
        [ProducesResponseType(typeof(ProductCategoryResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddProductCategory([FromBody] ProductCategoryRequest productCategoryRequest)
        {
            var response = await _productCategoryService.AddAsync(productCategoryRequest);
            await _cacheService.RemoveByPrefixAsync(_productCategoryService.ProductCategoryKey);
            return Ok(response);
        }
        #endregion Save

        #region Update
        [Authorize(Roles = "Admin")]
        [HttpPut("category/icon/{id}")]
        [ProducesResponseType(typeof(ProductCategoryResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCategoryIconAsync(IFormFile file, [FromRoute] int id)
        {
            var productCategory = await _productCategoryService.UpdateCategoryIconAsync(file, id);
            await _cacheService.RemoveByPrefixAsync(_productCategoryService.ProductCategoryKey);
            return Ok(productCategory);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("category/{id}")]
        [ProducesResponseType(typeof(ProductCategoryResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProductCategory([FromBody] ProductCategoryUpdateRequest productCategoryUpdateRequest, [FromRoute] int id)
        {
            var response = await _productCategoryService.UpdateAsync(productCategoryUpdateRequest, id, UserId);
            await _cacheService.RemoveByPrefixAsync(_productCategoryService.ProductCategoryKey);
            return Ok(response);
        }
        #endregion Update
    }
}
