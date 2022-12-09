using FarmFresh.Application.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FarmFresh.Application.Dto.Request.Products
{
    public class VoucherRequest
    {
        [Required]
        [StringLength(50)]
        [Description("Voucher Code")]
        public string VoucherCode { get; set; } = null!;

        [Required]
        [Description("Discount in percentage or fixed amount")]
        public decimal Discount { get; set; }

        [Required]
        [Description("Expiry Date of the voucher")]
        public DateTime ExpiryDate { get; set; }

        [Required]
        [Description("Start date of the voucher")]
        public DateTime StartDate { get; set; }

        [Required]
        [Description("Minimum amount to be spent to use this voucher")]
        public int MinimumAmount { get; set; }

        [Required]
        [Description("Maximum amount to be given by using this voucher")]
        public int MaximumAmount { get; set; }

        [Required]
        [Description("Number of times this voucher can be used")]
        public int UsageLimit { get; set; }

        [Required]
        [Description("Number of times this voucher has been used")]
        public int UsageCount { get; set; }

        [Required]
        [Description("Is this voucher active")]
        public bool IsActive { get; set; }

        [Required]
        [Description("Type of voucher")]
        public VoucherType VoucherType { get; set; }
    }
}
