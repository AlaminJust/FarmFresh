using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Dto.Response.Products;
using FarmFresh.Application.Interfaces.Services.Products;
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
        #endregion Properties

        #region Constructor

        public ProductCategoryController(
                IProductCategoryService productCategoryService
            )
        {
            _productCategoryService = productCategoryService;
        }
        #endregion Constructor

        #region Save
        [HttpPost("category")]
        [ProducesResponseType(typeof(ProductCategoryResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddProductCategory([FromBody] ProductCategoryRequest productCategoryRequest)
        {
            var response = await _productCategoryService.AddAsync(productCategoryRequest);
            return Ok(response);
        }
        #endregion Save

        #region Get
        [HttpGet("categories")]
        [ProducesResponseType(typeof(IEnumerable<ProductCategoryResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductCategories()
        {
            var response = await _productCategoryService.GetCategoriesTree();
            return Ok(response);
        }

        #endregion Get

    }
}
