using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Dto.Response.Products;
using FarmFresh.Application.Interfaces.Services.Caches;
using FarmFresh.Application.Interfaces.Services.Products;
using FarmFresh.Application.Models.Paginations.Products;
using FarmFresh.Application.Models.Paginations;
using FarmFresh.Infrastructure.Service.Services.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FarmFresh.Api.Controllers.Products
{
    [Route("api/product-category-management")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        #region Properties
        private readonly IProductCategoryService _productCategoryService;
        private readonly ICacheService _cacheService;
        #endregion Properties

        #region Constructor

        public ProductCategoryController(
                IProductCategoryService productCategoryService,
                ICacheService cacheService
            )
        {
            _productCategoryService = productCategoryService;
            _cacheService = cacheService;
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
