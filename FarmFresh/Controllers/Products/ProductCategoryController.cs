using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Dto.Response.Products;
using FarmFresh.Application.Interfaces.Services.Caches;
using FarmFresh.Application.Interfaces.Services.Images;
using FarmFresh.Application.Interfaces.Services.Products;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FarmFresh.Api.Controllers.Products
{
    [Route("api/product-category-management")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
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

        #region Save
        [HttpPost("category")]
        [ProducesResponseType(typeof(ProductCategoryResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddProductCategory([FromBody] ProductCategoryRequest productCategoryRequest)
        {
            var response = await _productCategoryService.AddAsync(productCategoryRequest);
            await _cacheService.RemoveByPrefixAsync(_productCategoryService.ProductCategoryKey);
            return Ok(response);
        }
        #endregion Save

        [HttpPut("category/icon/{id}")]
        [ProducesResponseType(typeof(ProductCategoryResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCategoryIcon(IFormFile file, [FromRoute] int id)
        {
            var productCategory = await _productCategoryService.UpdateCategoryIconAsync(file, id);
            return Ok(productCategory);
        }

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

    }
}
