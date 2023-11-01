using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmFresh.Domain.Entities.Users
{
    [Table("RefreshToken", Schema = "dbo")]
    public class RefreshToken : BaseEntity
    {
        [Column("Id"), Key]
        public int Id { get; set; }
        
        [Column("Token"), StringLength(255), Required]
        public string Token { get; set; }
        
        [Column("Expires"), Required]
        public DateTime Expires { get; set; }
        
        [Column("IsExpired")]
        public bool IsExpired => DateTime.UtcNow >= Expires;
        
        [Column("RevokedOn")]
        public DateTime? RevokedOn { get; set; }
        
        [Column("IsActive")]
        public bool IsActive => RevokedOn != null && !IsExpired;
        
        [Column("CreatedByIp"), StringLength(45)]
        public string CreatedByIp { get; set; }
        
        [Column("RevokedByIp"), StringLength(45)]
        public string RevokedByIp { get; set; }
        
        [Column("ReplacedByToken"), StringLength(255)]
        public string ReplacedByToken { get; set; }
        
        [Column("UserId"), Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [InverseProperty("RefreshToken")]
        public virtual User User { get; set; }
    }
}