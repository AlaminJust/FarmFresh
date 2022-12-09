using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FarmFresh.Email.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(Models.Email email);
    }
}
