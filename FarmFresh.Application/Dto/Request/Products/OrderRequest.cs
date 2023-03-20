using FarmFresh.Application.Enums;
using System.ComponentModel.DataAnnotations;

namespace FarmFresh.Application.Dto.Request.Products
{
    public class OrderRequest
    {
        [Required(ErrorMessage = "Payment method is required")]
        public PaymentMethod paymentMethod { get; set; }
        
        public string? transactionId { get; set; }
        
        public PaymentStatus paymentStatus { get; set; } = PaymentStatus.Pending;
        
        [Required(ErrorMessage = "Amount is required")]
        public decimal Amount { get; set; }
        
        [Required(ErrorMessage = "Address is required")]
        public string? Address { get; set; } = null!;
        
        public bool IsForceFullyOrder { get; set; } = false;
    }
}
