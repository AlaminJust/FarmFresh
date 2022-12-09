using FarmFresh.Application.Enums;
using System.ComponentModel;

namespace FarmFresh.Application.Dto.Response.Products
{
    public class VoucherResponse
    {
        public int Id { get; set; }

        [Description("Voucher Code")]
        public string VoucherCode { get; set; } = null!;

        [Description("Discount in percentage or fixed amount")]
        public decimal Discount { get; set; }

        [Description("Expiry Date of the voucher")]
        public DateTime ExpiryDate { get; set; }

        [Description("Start date of the voucher")]
        public DateTime StartDate { get; set; }

        [Description("Minimum amount to be spent to use this voucher")]
        public int MinimumAmount { get; set; }

        [Description("Maximum amount to be given by using this voucher")]
        public int MaximumAmount { get; set; }

        [Description("Number of times this voucher can be used")]
        public int UsageLimit { get; set; }

        [Description("Number of times this voucher has been used")]
        public int UsageCount { get; set; }

        [Description("Is this voucher active")]
        public bool IsActive { get; set; }

        [Description("Type of voucher")]
        public VoucherType VoucherType { get; set; }

        [Description("User who created this voucher")]
        public int CreatedBy { get; set; }
    }
}
