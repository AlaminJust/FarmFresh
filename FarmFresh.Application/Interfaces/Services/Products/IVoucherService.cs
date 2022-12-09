using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Dto.Response.Products;

namespace FarmFresh.Application.Interfaces.Services.Products
{
    public interface IVoucherService
    {
        #region Save
        Task<VoucherResponse> AddAsync(VoucherRequest voucherRequest, int userId);
        #endregion Save
    }
}
