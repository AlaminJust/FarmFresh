using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmFresh.Domain.Entities
{
    public partial class BaseEntity
    {
        [Column("CreatedOn", TypeName = "date")]
        public DateTime CreatedOn { get; set; }

        [Column("UpdatedOn", TypeName = "date")]
        public DateTime UpdatedOn { get; set; }
    }
}
