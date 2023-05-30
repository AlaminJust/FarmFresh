using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FarmFresh.Domain.Entities.Users;

namespace FarmFresh.Domain.Entities.Products;

[Table("PriceHistory", Schema = "dbo")]
public class PriceHistory: BaseEntity
{
    [Key, Column("Id")] 
    public int Id { get; set; }

    [Column("ProductId")]
    [ForeignKey("Product")]
    public int ProductId { get; set; }

    [Column("Price")]
    public float Price { get; set; }
    
    [Column("UpdateBy")]
    [ForeignKey("User")]
    public int UpdateBy { get; set; }
    
    public virtual Product Product { get; set; }
    public virtual User User { get; set; }
}