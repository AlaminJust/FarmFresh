﻿using AutoMapper;
using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Dto.Response.Products;
using FarmFresh.Application.Interfaces.Services.Products;
using FarmFresh.Domain.Entities.Products;
using FarmFresh.Domain.RepoInterfaces.Products;

namespace FarmFresh.Infrastructure.Service.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(
                IProductRepository productRepository,
                IMapper mapper
            )
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<ProductResponse> AddAsync(ProductRequest productRequest)
        {
            var product = _mapper.Map<Product>(productRequest);
            
            product.CreatedOn = DateTime.UtcNow;
            
            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();
            
            return _mapper.Map<ProductResponse>(product);
        }
    }
}
