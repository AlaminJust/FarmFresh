using FarmFresh.Application.Enums;
using FarmFresh.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmFresh.Domain.Entities.Products
{
    [Table("Order", Schema = "dbo")]
    public class Order: BaseEntity
    {
        public Order()
        {
            PaymentDetails = new HashSet<PaymentDetail>();
            OrderItems = new HashSet<OrderItem>();
        }
        
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("UserId")]
        [ForeignKey("User")]
        public int UserId { get; set; }
        
        [Column("TotalAmount")]
        public decimal TotalAmount { get; set; }

        [Column("DiscountAmount")]
        public decimal DiscountAmount { get; set; }

        [Column("NetAmount")]
        public decimal NetAmount { get; set; }

        [Column("PaymentStatus")]
        public PaymentStatus PaymentStatus { get; set; }

        [Column("OrderStatus")]
        public OrderStatus OrderStatus { get; set; }

        [Column("OrderDate", TypeName = "date")]
        public DateTime OrderDate { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<PaymentDetail> PaymentDetails { get; set; } = null!;
        public virtual ICollection<OrderItem> OrderItems { get; set; } = null!;
    }
}
