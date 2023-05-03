namespace FarmFresh.Application.Dto.Request.Users
{
    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; }
        public int UserId { get; set; }
    }
}
