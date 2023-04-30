namespace FarmFresh.Application.Dto.Response.Users
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string Token { get; set; } = null!;
        public string RefreshToken { get; set; }
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? ShippingAddress { get; set; }
        public string? BillingAddress { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + ' ' + LastName;
            }
        }
    }
}
