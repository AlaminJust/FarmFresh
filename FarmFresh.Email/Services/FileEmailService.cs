using FarmFresh.Application.Extensions;
using FarmFresh.Domain.Unity;
using FarmFresh.Email.Interfaces;
using FarmFresh.Email.Models;

namespace Taap.Email.Services
{
    public class FileEmailService : IEmailService
    {
        private readonly IEnviroment _enviroment;
        private readonly MailSettings _mailSetting;

        public FileEmailService(
                IEnviroment enviroment,
                MailSettings mailSetting
            )
        {
            _enviroment = enviroment;
            _mailSetting = mailSetting;
        }
        private string removeUnosupportedCharacter(string folderName)
        {
            folderName = folderName.Replace('<', '(').Replace('>', ')');
            return folderName;
        }
        public async Task SendEmail(FarmFresh.Email.Models.Email email)
        {
            await email.ToEmails.VisitAsync(to => _enviroment.EmailFileProvider.WriteTextToFileAsync($"emails/{removeUnosupportedCharacter(to)}/{removeUnosupportedCharacter(_mailSetting.EmailId)}/{Path.GetRandomFileName()}.html", email.Body));
        }
    }
}
