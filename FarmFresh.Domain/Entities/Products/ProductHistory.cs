using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FarmFresh.Application.Enums;
using FarmFresh.Domain.Entities.Users;

namespace FarmFresh.Domain.Entities.Products;

[Table("ProductHistory", Schema = "dbo")]
public class ProductHistory: BaseEntity
{
    [Key, Column("Id")] 
    public int Id { get; set; }

    [Column("ProductId")]
    [ForeignKey("Product")]
    public int ProductId { get; set; }

    [Column("Point")]
    public float Point { get; set; }
    
    [Column("UpdateBy")]
    [ForeignKey("User")]
    public int UpdateBy { get; set; }
    
    [Column("HistoryType")]
    public ProductHistoryType HistoryType { get; set; }
    
    public virtual Product Product { get; set; }
    public virtual User User { get; set; }
}