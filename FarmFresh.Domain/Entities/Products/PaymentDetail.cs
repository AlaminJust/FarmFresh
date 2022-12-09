using FarmFresh.Application.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmFresh.Domain.Entities.Products
{
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
        public string TransactionId { get; set; } = null!;
        
        [Column("Date", TypeName = "date")]
        public DateTime PaymentDate { get; set; }

        [Column("PaymentStatus")]
        [Description("Payment Status")]
        public PaymentStatus PaymentStatus { get; set; }
        public virtual Order Order { get; set; } = null!;
    }
}
