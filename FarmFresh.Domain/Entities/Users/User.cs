using System.Collections;
using FarmFresh.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmFresh.Domain.Entities.Users
{
    [Table("User", Schema = "dbo")]
    [Index(nameof(Email), Name = "IX_User_Email", IsUnique = true)]
    [Index(nameof(UserName), Name = "IX_User_Username", IsUnique = true)]
    public partial class User : BaseEntity
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
            ProductsCreated = new HashSet<Product>();
            ProductsUpdated = new HashSet<Product>();
            CartItems = new HashSet<CartItem>();
            Vouchers = new HashSet<Voucher>();
            Orders = new HashSet<Order>();
            RefreshTokens = new HashSet<RefreshToken>();
            Locations = new HashSet<Location>();
            PriceHistories = new HashSet<PriceHistory>();
        }
        
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("UserName")]
        [StringLength(20)]
        [Required]
        public string UserName { get; set; } = null!;
        [Column("FirstName")]
        [StringLength(50)]
        [Required]
        [Unicode(true)]
        public string? FirstName { get; set; }
        [Column("LastName")]
        [StringLength(50)]
        [Unicode(true)]
        public string? LastName { get; set; }
        [Column("Email")]
        [StringLength(50)]
        [Required]
        [Unicode(true)]
        public string? Email { get; set; }

        [Column("PhoneNumber")]
        [StringLength(20)]
        [Required]
        [Unicode(true)]
        public string PhoneNumber { get; set; } = null!;

        [Column("Password")]
        [StringLength(128)]
        [Required]
        public string Password { get; set; } = null!;

        [Column("ShippingAddress")]
        [StringLength(150)]
        public string? ShippingAddress { get; set; }

        [Column("BillingAddress")]
        [StringLength(150)]
        public string? BillingAddress { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        
        [InverseProperty("UserCreatedBy")]
        public virtual ICollection<Product> ProductsCreated { get; set; }
        [InverseProperty("UserUpdatedBy")]
        public virtual ICollection<Product> ProductsUpdated { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<Voucher> Vouchers { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<PriceHistory> PriceHistories { get; set; }
    }
}
