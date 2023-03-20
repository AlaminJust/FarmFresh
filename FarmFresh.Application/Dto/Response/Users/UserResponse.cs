namespace FarmFresh.Application.Dto.Response.Users
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string? ShippingAddress { get; set; }
        public string? BillingAddress { get; set; }
    }
}
