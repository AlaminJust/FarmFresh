using FarmFresh.Application.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmFresh.Domain.Entities.Products
{
    public class Voucher: BaseEntity
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column("VoucherCode")]
        [Description("Voucher Code")]
        public string VoucherCode { get; set; } = null!;

        [Required]
        [Column("Discount")]
        [Description("Discount in percentage or fixed amount")]
        public decimal Discount { get; set; }

        [Required]
        [Column("ExpiryDate", TypeName = "date")]
        [Description("Expiry Date of the voucher")]
        public DateTime ExpiryDate { get; set; }

        [Required]
        [Column("StartDate", TypeName = "date")]
        [Description("Start date of the voucher")]
        public DateTime StartDate { get; set; }

        [Required]
        [Column("MinimumAmount")]
        [Description("Minimum amount to be spent to use this voucher")]
        public int MinimumAmount { get; set; }

        [Required]
        [Column("MaximumAmount")]
        [Description("Maximum amount to be given by using this voucher")]
        public int MaximumAmount { get; set; }

        [Required]
        [Column("UsageLimit")]
        [Description("Number of times this voucher can be used")]
        public int UsageLimit { get; set; }

        [Required]
        [Column("UsageCount")]
        [Description("Number of times this voucher has been used")]
        public int UsageCount { get; set; }

        [Required]
        [Column("IsActive")]
        [Description("Is this voucher active")]
        public bool IsActive { get; set; }

        [Required]
        [Column("VoucherType")]
        [Description("Type of voucher")]
        public VoucherType VoucherType { get; set; }
    }
}
