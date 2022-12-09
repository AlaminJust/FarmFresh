using FarmFresh.Email.Models;

namespace FarmFresh.Email.Interfaces
{
    public interface ITemplateService
    {
        Task SendEmail(string email, TemplatesDetails tempalteNames, BaseTemplate data);
    }
}
