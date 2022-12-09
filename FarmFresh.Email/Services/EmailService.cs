using FarmFresh.Email.Interfaces;
using FarmFresh.Email.Models;
using System.Net.Mail;

namespace FarmFresh.Email.Services
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _mailSetting;

        public EmailService(
                MailSettings mailSetting
            )
        {
            _mailSetting = mailSetting;
        }
        
        public async Task SendEmail(Models.Email email)
        {
            MailMessage m = new MailMessage();
            SmtpClient sc = new System.Net.Mail.SmtpClient();
            
            m.From = new MailAddress(_mailSetting.EmailId, _mailSetting.FromName);
            foreach (var Email in email.ToEmails)
            {
                m.To.Add(Email);
            }
            
            m.Subject = email.Subject;
            m.Body = email.Body;
            m.IsBodyHtml = email.IsHtml;
            
            sc.Host = _mailSetting.Host;

            if (email.Attachments is not null)
            {
                byte[] fileBytes;
                foreach (var file in email.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        Stream stream = new MemoryStream(fileBytes);
                        var attachMent = new Attachment(stream, file.FileName, file.ContentType);
                        m.Attachments.Add(attachMent);
                    }
                }
            }
            
            string str1 = "gmail.com";
            string str2 = _mailSetting.EmailId.ToLower();
            
            if (str2.Contains(str1))
            {
                try
                {
                    sc.Port = 587;
                    sc.Credentials = new System.Net.NetworkCredential(_mailSetting.EmailId, _mailSetting.Password);
                    sc.EnableSsl = _mailSetting.EnableSsl;
                    await sc.SendMailAsync(m);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                try
                {
                    sc.Port = _mailSetting.Port;
                    sc.Credentials = new System.Net.NetworkCredential(_mailSetting.EmailId, _mailSetting.Password);
                    sc.EnableSsl = _mailSetting.EnableSsl;
                    await sc.SendMailAsync(m);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
