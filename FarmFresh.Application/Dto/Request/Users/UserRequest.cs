using BC = BCrypt.Net.BCrypt;


namespace FarmFresh.Application.Dto.Request.Users
{
    public class UserRequest
    {
        private string _password { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string Password
        {
            get
            {
                return BC.HashPassword(_password);
            }
            set
            {
                this._password = value;
            }
        }
    }
}
