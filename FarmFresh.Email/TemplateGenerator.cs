using FarmFresh.Email.Interfaces;
using FarmFresh.Email.Models;
using HandlebarsDotNet;
using Taap.Email.FileProviders;

namespace Taap.Email
{
    public class TemplateGenerator
    {
        private readonly IEmailService _emailService;

        public TemplateGenerator(
                IEmailService emailService
            )
        {
            _emailService = emailService;
        }

        private string GetTemplateFromData(string template, BaseTemplate data)
        {
            var templateObject = Handlebars.Compile(template);
            return templateObject(data);
        }

        public async Task<string> PrepareHtmlBodyAsync(string filename, BaseTemplate Data)
        {
            if (Directory.Exists("/wwwroot/email-templates") && File.Exists($"/wwwroot/email-templates/{filename}.html"))
            {
                FileProvider provider = new FileProvider("\\wwwroot\\email-templates");
                var template = await provider.ReadFileToEndAsync(filename);
                return GetTemplateFromData(template, Data);
            }
            else
            {
                throw new FileNotFoundException("Template not found");
            }
        }

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            FarmFresh.Email.Models.Email mail = new FarmFresh.Email.Models.Email(email, subject, body);
            await _emailService.SendEmail(mail);
        }
    }
}
