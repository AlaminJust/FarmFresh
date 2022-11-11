using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        [Unicode(false)]
        public string? FirstName { get; set; }
        [Column("LastName")]
        [StringLength(50)]
        [Unicode(false)]
        public string? LastName { get; set; }
        [Column("Email")]
        [StringLength(50)]
        [Required]
        [Unicode(false)]
        public string? Email { get; set; }
        [Column("Password")]
        [StringLength(128)]
        [Required]
        public string? Password { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
