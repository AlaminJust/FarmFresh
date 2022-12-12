using FarmFresh.Domain.Entities.Products;

namespace FarmFresh.Domain.RepoInterfaces.Products
{
    public interface IVoucherRepository: IBaseRepository<Voucher>
    {
        #region Get
        Task<decimal> ApplyVoucherAsync(int voucherId, decimal totalPrice);
        #endregion Get
    }
}
