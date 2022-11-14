using AutoMapper;
using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Dto.Response.Products;
using FarmFresh.Application.Models.Paginations;
using FarmFresh.Domain.Entities.Products;

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
            #endregion Products
        }
    }
}
