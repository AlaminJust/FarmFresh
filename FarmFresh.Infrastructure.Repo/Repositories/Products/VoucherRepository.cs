using FarmFresh.Application.Enums;
using FarmFresh.Domain.Entities.Products;
using FarmFresh.Domain.RepoInterfaces.Products;
using FarmFresh.Infrastructure.Data.DbContexts;

namespace FarmFresh.Infrastructure.Repo.Repositories.Products
{
    public class VoucherRepository : BaseRepository<Voucher>, IVoucherRepository
    {
        #region Properties
        private readonly EFDbContext _context;
        #endregion Properties

        #region Constructor
        public VoucherRepository(EFDbContext context) : base(context)
        {
            _context = context;
        }
        #endregion Constructor

        #region Private Method
        private bool IsValid(Voucher voucher)
        {
            bool isValid = true;
            
            if (voucher is null || voucher.IsDeleted || !voucher.IsActive)
            {
                isValid = false;
            }
            else if (voucher.ExpiryDate < DateTime.Now || voucher.StartDate > DateTime.Now)
            {
                isValid = false;
            }
            else if (voucher.UsageCount >= voucher.UsageLimit)
            {
                isValid = false;
            }

            return isValid;
        }
        #endregion Private Method

        #region Get
        public async Task<decimal> ApplyVoucherAsync(int voucherId, decimal totalPrice)
        {
            Voucher voucher = await GetByIdAsync(voucherId);
            decimal discountOfVoucher = 0;

            if (!IsValid(voucher))
            {
                return discountOfVoucher = 0;
            }

            switch (voucher.VoucherType)
            {
                case VoucherType.Percentage:
                    discountOfVoucher = (totalPrice * voucher.VoucherValue / 100);
                    break;
                case VoucherType.Fixed:
                    discountOfVoucher = ( totalPrice - voucher.VoucherValue );
                    break;
                default:
                    break;
            }

            return discountOfVoucher;
        }
        #endregion Get
    }
}
