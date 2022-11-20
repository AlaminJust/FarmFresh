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

        private string GetTemplateFromData(string Template, BaseTemplate Data)
        {
            var templateObject = Handlebars.Compile(Template);
            return templateObject(Data);
        }

        public async Task<string> PrepareHtmlBodyAsync(string Filename, BaseTemplate Data)
        {
            if (Directory.Exists("/wwwroot/email-templates") && File.Exists($"/wwwroot/email-templates/{Filename}.html"))
            {
                FileProvider provider = new FileProvider("\\wwwroot\\email-templates");
                var template = await provider.ReadFileToEndAsync(Filename);
                return GetTemplateFromData(template, Data);
            }
            else
            {
                throw new FileNotFoundException("Template not found");
            }
        }

        public async Task SendEmailAsync(string Email, string Subject, string Body)
        {
            FarmFresh.Email.Models.Email email = new FarmFresh.Email.Models.Email(Email, Subject, Body);
            await _emailService.SendEmail(email);
        }
    }
}
