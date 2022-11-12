using FarmFresh.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace FarmFresh.Domain.Entities.Users
{
    [Table("Role", Schema = "dbo")]
    public partial class Role : BaseEntity
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
            RoleType = RoleType.Customer;
        }
        
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("RoleType")]
        [Required]
        public RoleType RoleType { get; set; }
        [Column("Name")]
        [StringLength(20)]
        [Required]
        public string Name { get; set; } = null!;
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
