using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmFresh.Domain.Entities.Products
{
    [Table("Cart", Schema = "dbo")]
    public class Cart: BaseEntity
    {
        public Cart()
        {
            CartItems = new HashSet<CartItem>();
        }
        
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        
        [Column("UserId")]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Column("TotalPrice")]
        public decimal TotalPrice { get; set; }

        [Column("DiscountPrice")]
        public decimal DiscountPrice { get; set; }

        [Column("VoucherId")]
        [ForeignKey("Voucher")]
        public int? VoucherId { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; } = null!;
        public virtual Voucher? Voucher { get; set; }

    }
}
