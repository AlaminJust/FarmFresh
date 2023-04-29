using AutoMapper;
using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Dto.Response.Products;
using FarmFresh.Application.Interfaces.Services.Images;
using FarmFresh.Application.Interfaces.Services.Products;
using FarmFresh.Application.Models.Paginations;
using FarmFresh.Application.Models.Paginations.Products;
using FarmFresh.Domain.Entities.Products;
using FarmFresh.Domain.RepoInterfaces.Products;
using Microsoft.AspNetCore.Http;

namespace FarmFresh.Infrastructure.Service.Services.Products
{
    public class ProductService : IProductService
    {
        #region Properties
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public string ImageUrlPath => "wwwroot\\images\\products\\";
        private string RootPath => "images/products/";
        #endregion Properties

        #region Constructor

        public ProductService(
                IProductRepository productRepository,
                IMapper mapper,
                IImageService imageService
            )
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _imageService = imageService;
        }
        #endregion Constructor

        #region Get
        public async Task<ProductDetailsResponse> GetProductDetailsByIdAsync(int id)
        {
            var productDetails = await _productRepository.GetProductDetailsByIdAsync(id);
            return _mapper.Map<ProductDetailsResponse>(productDetails);
        }

        public async Task<PaginationResponse<ProductResponse>> GetPaginatedProductsAsync(ProductPaginationRequest productPaginationRequest)
        {
            var products = await _productRepository.GetPaginatedProductsAsync(productPaginationRequest);
            return _mapper.Map<PaginationResponse<ProductResponse>>(products);
        }

        public Task<bool> IsAvailableInStockAsync(int productId, int quantity)
        {
            return _productRepository.IsAvailableInStockAsync(productId, quantity);
        }

        #endregion Get

        #region Save
        public async Task<ProductResponse> AddAsync(ProductRequest productRequest, int userId)
        {
            var product = _mapper.Map<Product>(productRequest);
            
            product.CreatedOn = DateTime.UtcNow;
            product.CreatedBy = userId;
            product.UpdatedBy = userId;
            product.UpdatedOn = DateTime.UtcNow;

            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();
            
            return _mapper.Map<ProductResponse>(product);
        }
        #endregion Save

        #region Update
        public async Task UpdateProductStockAsync(int productId, int quantity)
        {
            await _productRepository.UpdateProductStockAsync(productId, quantity);
        }

        public async Task<ProductResponse> UpdateProductImageAsync(IFormFile file, int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            
            if(product is null)
            {
                throw new NullReferenceException("Product not found.");
            }

            if(file is null)
            {
                throw new Exception("File is required to save image of the product.");
            }
            
            product.ImageUrls = RootPath + await _imageService.SaveToFolderAsync(file, ImageUrlPath);
            await _productRepository.UpdateAsync(product);
            await _productRepository.SaveChangesAsync();
            return _mapper.Map<ProductResponse>(product);
        }
        #endregion Update
    }
}
