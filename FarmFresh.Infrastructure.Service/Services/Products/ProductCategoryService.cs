using AutoMapper;
using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Dto.Response.Products;
using FarmFresh.Application.Interfaces.Services.Images;
using FarmFresh.Application.Interfaces.Services.Products;
using FarmFresh.Domain.Entities.Products;
using FarmFresh.Domain.RepoInterfaces.Products;
using Microsoft.AspNetCore.Http;

namespace FarmFresh.Infrastructure.Service.Services.Products
{
    public class ProductCategoryService : IProductCategoryService
    {
        #region Properties
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public string ProductCategoryKey => "ProductCategoryKey";
        public string FolderPathUrl => "wwwroot\\images\\product-category\\";
        private string RootPath => "images/product-category/";
        #endregion Properties

        #region Constructor
        public ProductCategoryService(
                IProductCategoryRepository productCategoryRepository,
                IMapper mapper,
                IImageService imageService
            )
        {
            _productCategoryRepository = productCategoryRepository;
            _mapper = mapper;
            _imageService = imageService;
        }

        #endregion Constructor

        #region Get
        public async Task<IEnumerable<ProductCategoryResponse>> GetCategoriesTree()
        {
            var productCategories = await _productCategoryRepository.GetCategoryList();

            var response = _mapper.Map<IEnumerable<ProductCategoryResponse>>(productCategories);
            
            foreach(var item in response)
            {
                item.UpdateParentCategoryName("Category");
            }

            return response;
        }

        #endregion Get

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

        #region Update
        public async Task<ProductCategoryResponse> UpdateCategoryIconAsync(IFormFile file, int id)
        {
            var productCategory = await _productCategoryRepository.GetByIdAsync(id);

            if (productCategory == null)
            {
                throw new NullReferenceException("Product category not found");
            }

            if (file == null)
            {
                throw new NullReferenceException("File is required");
            }

            if (file.Length > 500000) // 500KB
            {
                throw new Exception("The image upload failed because the image was too big(max 0.5MB)");
            }

            productCategory.IconUrl = RootPath + await _imageService.SaveToFolderAsync(file, FolderPathUrl);

            await _productCategoryRepository.UpdateAsync(productCategory);
            await _productCategoryRepository.SaveChangesAsync();

            return _mapper.Map<ProductCategoryResponse>(productCategory);
        }

        public async Task<ProductCategoryResponse> UpdateAsync(ProductCategoryUpdateRequest productCategoryRequest, int id, int userId)
        {
            var productCategory = await _productCategoryRepository.GetByIdAsync(id);

            if (productCategory == null)
            {
                throw new NullReferenceException("Product category not found");
            }

            productCategory.CategoryName = productCategoryRequest.CategoryName ?? productCategory.CategoryName;
            productCategory.CategoryDescription = productCategoryRequest.CategoryDescription;
            productCategory.ParentCategoryId = productCategoryRequest.ParentCategoryId ?? productCategory.ParentCategoryId;
            productCategory.UpdatedOn = DateTime.UtcNow;

            await _productCategoryRepository.UpdateAsync(productCategory);
            await _productCategoryRepository.SaveChangesAsync();

            return _mapper.Map<ProductCategoryResponse>(productCategory);
        }
        #endregion Update

    }
}
