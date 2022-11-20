namespace FarmFresh.Email.Models
{
    public class MailSettings
    {
        public string Host { get; set; } = null!;
        public int Port { get; set; }
        public string? FromName { get; set; }
        public string EmailId { get; set; } = null!;
        public string? Password { get; set; }
        public bool EnableSsl { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public bool EnableMailService { get; set; }
    }
}
