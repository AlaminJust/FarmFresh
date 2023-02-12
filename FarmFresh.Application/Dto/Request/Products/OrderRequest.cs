using FarmFresh.Application.Enums;

namespace FarmFresh.Application.Dto.Request.Products
{
    public class OrderRequest
    {
        public int userId { get; set; }
        public PaymentMethod paymentMethod { get; set; }
        public string? transactionId { get; set; }
        public PaymentStatus paymentStatus { get; set; } = PaymentStatus.Pending;
        public decimal Amount { get; set; }
    }
}
