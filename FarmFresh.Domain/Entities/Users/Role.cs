using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmFresh.Domain.Entities.Users
{
    [Table("Role", Schema = "dbo")]
    public partial class Role : BaseEntity
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }
        
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Name")]
        [StringLength(20)]
        [Required]
        public string Name { get; set; } = null!;
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
