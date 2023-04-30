using System.ComponentModel.DataAnnotations;

namespace FarmFresh.Domain.Dto.Request.Users
{
    public class LoginRequest
    {
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
