using FarmFresh.Application.Dto.Response.Products;
using FarmFresh.Domain.Entities.Products;
using FarmFresh.Domain.Entities.Users;
using FarmFresh.Domain.RepoInterfaces.Products;
using FarmFresh.Domain.ResponseEntities.Products;
using FarmFresh.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace FarmFresh.Infrastructure.Repo.Repositories.Products
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly EFDbContext _context;

        public OrderRepository(EFDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<List<OrderDetails>> GetOrdersByUserIdAsync(int userId)
        {
            var orders = (from o in _context.Orders.AsNoTracking()
                          join u in _context.Users.AsNoTracking() on o.UserId equals u.Id
                          where (u.Id == userId)
                          select new OrderDetails
                          {
                              Id = o.Id,
                              UserId = u.Id,
                              Address = o.Address,
                              DiscountAmount = o.DiscountAmount,
                              NetAmount = o.NetAmount,
                              TotalAmount = o.TotalAmount,
                              OrderDate = o.OrderDate,
                              OrderStatus = o.OrderStatus.ToString(),
                              PaymentStatus = o.PaymentStatus.ToString(),
                              PhoneNumber = u.PhoneNumber,
                              UserName = u.UserName,
                              OrderItemResponse = o.OrderItems.Select(oi => new OrderItem
                              {
                                  Id = oi.Id,
                                  OrderId = oi.OrderId,
                                  ProductId = oi.ProductId,
                                  Quantity = oi.Quantity,
                                  Price = oi.Price,
                                  Total = oi.Total,
                                  Discount = oi.Discount,
                                  Product = oi.Product
                              }).ToList()
                          }).OrderByDescending(x => x.OrderDate).ToList();

            return Task.FromResult(orders);
        }

        public Task<List<OrderDetails>> GetOrdersOfAllUsersAsync()
        {
            var orders = (from o in _context.Orders
                          join u in _context.Users on o.UserId equals u.Id
                          where (u.IsDeleted == false)
                          select new OrderDetails
                          {
                              Id = o.Id,
                              UserId = u.Id,
                              Address = o.Address,
                              DiscountAmount = o.DiscountAmount,
                              NetAmount = o.NetAmount,
                              TotalAmount = o.TotalAmount,
                              OrderDate = o.OrderDate,
                              OrderStatus = o.OrderStatus.ToString(),
                              PaymentStatus = o.PaymentStatus.ToString(),
                              PhoneNumber = u.PhoneNumber,
                              UserName = u.UserName,
                          }).OrderByDescending(x => x.OrderDate).ToList();

            return Task.FromResult(orders);
        }
    }
}
