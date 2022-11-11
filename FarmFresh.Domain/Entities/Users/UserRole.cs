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
    [Table("UserRole", Schema = "dbo")]
    public partial class UserRole : BaseEntity
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [ForeignKey("User")]
        [Column("UserId")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        
        [ForeignKey("Role")]
        [Column("RoleId")]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        
        [Column("IsActive")]
        public bool IsActive { get; set; }
    }
}
