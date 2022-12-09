using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Interfaces.Services.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FarmFresh.Api.Controllers.Products
{
    [Route("api/voucher-management")]
    [ApiController]
    public class VoucherController : ApiControllerBase
    {
        #region Properties
        private readonly IVoucherService _voucherService;
        #endregion Properties

        #region Constructor
        public VoucherController(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }
        #endregion Constructor

        #region Save
        [HttpPost("voucher")]
        [Authorize]
        public async Task<IActionResult> AddAsync([FromBody] VoucherRequest voucherRequest)
        {
            var response = await _voucherService.AddAsync(voucherRequest, UserId);
            return Ok(response);
        }
        #endregion Save
    }
}