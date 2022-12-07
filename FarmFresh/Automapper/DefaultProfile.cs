using AutoMapper;
using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Dto.Response.Products;
using FarmFresh.Application.Models.Paginations;
using FarmFresh.Domain.Entities.Products;
using FarmFresh.Domain.ResponseEntities.Products;

namespace FarmFresh.Api.Automapper
{
    public class DefaultProfile : Profile
    {
        public DefaultProfile()
        {
            #region Products
            CreateMap<ProductRequest, Product>();
            CreateMap<Product, ProductResponse>();
            CreateMap<PaginationResponse<Product>, PaginationResponse<ProductResponse>>();
            CreateMap<ProductDetails, ProductDetailsResponse>();
            CreateMap<Discount, DiscountResponse>();
            CreateMap<DiscountRequest, Discount>();
            #endregion Products
        }
    }
}
