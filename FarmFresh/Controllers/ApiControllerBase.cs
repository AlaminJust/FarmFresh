using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FarmFresh.Api.Controllers
{
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {
        public int UserId => int.Parse(
           string.IsNullOrEmpty(User.FindFirstValue("UserId")) ? "0" : User.FindFirstValue("UserId"));
    }
}
