using AutoMapper;
using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Dto.Response.Products;
using FarmFresh.Application.Interfaces.Services.Products;
using FarmFresh.Domain.Entities.Products;
using FarmFresh.Domain.RepoInterfaces.Products;

namespace FarmFresh.Infrastructure.Service.Services.Products
{
    public class ProductCategoryService : IProductCategoryService
    {
        #region Properties
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IMapper _mapper;
        public string ProductCategoryKey => "ProductCategoryKey";
        #endregion Properties

        #region Constructor
        public ProductCategoryService(
                IProductCategoryRepository productCategoryRepository,
                IMapper mapper
            )
        {
            _productCategoryRepository = productCategoryRepository;
            _mapper = mapper;
        }

        #endregion Constructor

        #region Save
        public async Task<ProductCategoryResponse> AddAsync(ProductCategoryRequest productCategoryRequest)
        {
            var productCategory = _mapper.Map<ProductCategory>(productCategoryRequest);

            productCategory.CreatedOn = DateTime.UtcNow;

            await _productCategoryRepository.AddAsync(productCategory);
            await _productCategoryRepository.SaveChangesAsync();

            return _mapper.Map<ProductCategoryResponse>(productCategory);
        }

        #endregion Save

        #region Get
        public async Task<IEnumerable<ProductCategoryResponse>> GetCategoriesTree()
        {
            var productCategories = await _productCategoryRepository.GetCategoryList();

            return _mapper.Map<IEnumerable<ProductCategoryResponse>>(productCategories);
        }
        #endregion Get
    }
}
