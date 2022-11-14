using FarmFresh.Application.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
