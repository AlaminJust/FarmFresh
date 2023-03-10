using FarmFresh.Application.Enums;

namespace FarmFresh.Domain.ResponseEntities.Products
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal NetAmount { get; set; }
        public string PaymentStatus { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string OrderStatus { get; set; } = null!;
        public DateTime OrderDate { get; set; }
    }
}
