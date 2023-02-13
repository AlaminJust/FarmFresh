using FarmFresh.Application.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmFresh.Domain.Entities.Products
{
    [Table("PaymentDetail", Schema = "dbo")]
    public class PaymentDetail: BaseEntity
    {
        [Key, Required]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column("OrderId")]
        [ForeignKey("Order")]
        public int OrderId { get; set; }

        [Column("Amount")]
        [Description("Amount")]
        public decimal Amount { get; set; }
        
        [Column("PaymentMethod")]
        [Description("Payment Method")]
        public PaymentMethod PaymentMethod { get; set; }

        [Column("TransactionId")]
        [Required]
        [Description("Transaction Id")]
        public string? TransactionId { get; set; }
        public int? VoucherId { get; set; }
        public virtual Voucher? Voucher { get; set; }

        [Column("Date", TypeName = "date")]
        public DateTime PaymentDate { get; set; }
        public virtual Order Order { get; set; } = null!;
        public decimal? VoucherDiscount { get; set; }
    }
}
