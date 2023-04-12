namespace FarmFresh.Application.Dto.Response.Products
{
    public class OrderResponse
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
        public List<OrderItemResponse> OrderItemResponse { get; set; } = null!;
    }
    
    public class OrderItemResponse
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public ProductResponse Product { get; set; } = null!;
    }
}
