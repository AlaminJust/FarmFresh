using FarmFresh.Application.Dto.Request.Products;
using System.Runtime.CompilerServices;

namespace FarmFresh.Application.Interfaces.Services.Products
{
    public interface IOrderService
    {
        Task<Int32> OrderAsync(OrderRequest orderRequest);
    }
}
