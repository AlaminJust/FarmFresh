using Microsoft.AspNetCore.Http;

namespace FarmFresh.Email.Models
{
    public class Email
    {
        public IEnumerable<string> ToEmails { get; set; } = new List<string>();
        public string? Subject { get; set; }
        public string Body { get; set; } = null!;
        public IEnumerable<IFormFile>? Attachments { get; set; } = new List<IFormFile>();
        public Boolean IsHtml { get; set; } = true;

        public Email(string ToEmail)
        {
            ToEmails = ToEmails.Append(ToEmail);
        }

        public Email(string _ToEmail, string _Subject, string _Body)
        {
            Body = _Body;
            Subject = _Subject;
            ToEmails = ToEmails.Append(_ToEmail);
        }

        public Email(string _ToEmail, string _Subject)
        {
            ToEmails = ToEmails.Append(_ToEmail);
            Subject = _Subject;
        }
    }
}
