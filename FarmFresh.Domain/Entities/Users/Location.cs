using FarmFresh.Application.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmFresh.Domain.Entities.Users
{
    [Table("Location", Schema = "dbo")]
    public class Location: BaseEntity
    {
        [Column("Id"), Key]
        public int Id { get; set; }

        [Column("UserId")]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Column("Name"), StringLength(255)]
        public string Name { get; set; }

        [Column("Latitude", TypeName = "decimal(18, 15)")]
        public decimal? Latitude { get; set; }

        [Column("Longitude", TypeName = "decimal(18, 15)")]
        public decimal? Longitude { get; set; }

        [Column("Speed")]
        public float? Speed { get; set; }

        [Column("Altitude")]
        public double? Altitude { get; set; }

        [Column("Accuracy")]
        public float? Accuracy { get; set; }

        [Column("Address"), StringLength(255)]
        public string Address { get; set; }

        [Column("City"), StringLength(50)]
        public string City { get; set; }

        [Column("State"), StringLength(50)]
        public string State { get; set; }

        [Column("Country"), StringLength(50)]
        public string Country { get; set; }

        [Column("ZipCode"), StringLength(10)]
        public string ZipCode { get; set; }
        [Column("LocationType")]
        public LocationType LocationType { get; set; }
        public virtual User User { get; set; }
    }
}