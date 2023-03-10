using FarmFresh.Application.Dto.Response.Products;
using FarmFresh.Domain.Entities.Products;
using FarmFresh.Domain.ResponseEntities.Products;

namespace FarmFresh.Domain.RepoInterfaces.Products
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<List<OrderDetails>> GetOrdersByUserIdAsync(int userId);
        Task<List<OrderDetails>> GetOrdersOfAllUsersAsync();
    }
}
