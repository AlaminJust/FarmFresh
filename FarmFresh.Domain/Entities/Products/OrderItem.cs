using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmFresh.Domain.Entities.Products
{
    [Table("OrderItem", Schema = "dbo")]
    public class OrderItem: BaseEntity
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column("OrderId")]
        [ForeignKey("Order")]
        [Description("Foreign key for the Order that the OrderItem belongs to")]
        public int OrderId { get; set; }

        [Required]
        [Column("ProductId")]
        [ForeignKey("Product")]
        [Description("Foreign key for the Product that the OrderItem represents")]
        public int ProductId { get; set; }

        [Column("Quantity")]
        [Description("Quantity of the Product in the OrderItem")]
        public int Quantity { get; set; }

        [Column("Price")]
        [Description("Price of the Product in the OrderItem")]
        public decimal Price { get; set; }

        [Column("Discount")]
        [Description("Discount of the Product in the OrderItem")]
        public decimal Discount { get; set; }

        [Column("Total")]
        [Description("Total of the Product in the OrderItem")]
        public decimal Total { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
