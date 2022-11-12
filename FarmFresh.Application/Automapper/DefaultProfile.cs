using AutoMapper;
using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Dto.Response.Products;
using FarmFresh.Domain.Entities.Products;

namespace FarmFresh.Application.Automapper
{
    public class DefaultProfile : Profile
    {
        public DefaultProfile()
        {
            #region Products
            CreateMap<ProductRequest, Product>();
            CreateMap<Product, ProductResponse>();
            #endregion Products
        }
    }
}
