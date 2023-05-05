using System.ComponentModel.DataAnnotations.Schema;

namespace FarmFresh.Domain.Entities
{
    public partial class BaseEntity
    {
        [Column("CreatedOn", TypeName = "date")]
        public DateTime CreatedOn { get; set; }

        [Column("UpdatedOn", TypeName = "date")]
        public DateTime UpdatedOn { get; set; }

        [Column("IsDeleted")]
        public bool IsDeleted { get; set; }
    }
}
