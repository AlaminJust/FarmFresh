using FarmFresh.Email.Interfaces;
using FarmFresh.Email.Models;

namespace Taap.Email.Services
{
    public class TemplateService : ITemplateService
    {
        private readonly TemplateGenerator _templateGenerator;

        public TemplateService(
                TemplateGenerator templateGenerator
            )
        {
            _templateGenerator = templateGenerator;
        }
        public async Task SendEmail(string email, TemplatesDetails TempalteNames, BaseTemplate data)
        {
            var body = await _templateGenerator.PrepareHtmlBodyAsync(TempalteNames.Name, data);
            await _templateGenerator.SendEmailAsync(email, TempalteNames.Subject, body);
        }
    }
}
