using AutoMapper;
using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Dto.Response.Products;
using FarmFresh.Application.Interfaces.Services.Products;
using FarmFresh.Domain.Entities.Products;
using FarmFresh.Domain.RepoInterfaces.Products;

namespace FarmFresh.Infrastructure.Service.Services.Products
{
    public class VoucherService : IVoucherService
    {
        #region Properties
        private readonly IVoucherRepository _voucherRepository;
        private readonly IMapper _mapper;
        #endregion Properties

        #region Constructor
        public VoucherService(
                IVoucherRepository voucherRepository,
                IMapper mapper
            )
        {
            _voucherRepository = voucherRepository;
            _mapper = mapper;
        }
        #endregion Constructor

        #region Save
        public async Task<VoucherResponse> AddAsync(VoucherRequest voucherRequest, int userId)
        {
            Voucher voucher = _mapper.Map<Voucher>(voucherRequest);

            voucher.CreatedBy = userId;
            voucher.CreatedOn = DateTime.UtcNow;

            await _voucherRepository.AddAsync(voucher);
            await _voucherRepository.SaveChangesAsync();

            return _mapper.Map<VoucherResponse>(voucher);
        }
        #endregion Save
    }
}
