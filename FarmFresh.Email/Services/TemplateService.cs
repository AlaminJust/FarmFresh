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
        public async Task SendEmail(string Email, TemplatesDetails TempalteNames, BaseTemplate Data)
        {
            var Body = await _templateGenerator.PrepareHtmlBodyAsync(TempalteNames.Name, Data);
            await _templateGenerator.SendEmailAsync(Email, TempalteNames.Subject, Body);
        }
    }
}
