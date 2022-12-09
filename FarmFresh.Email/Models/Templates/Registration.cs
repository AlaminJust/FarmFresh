using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmFresh.Email.Models.Templates
{
    public class Registration : BaseTemplate
    {
        public string UserName { get; set; }
        public string ActivationLink { get; set; }

        public Registration(
                string userName,
                string activationLink
            )
        {
            UserName = userName;
            ActivationLink = activationLink;
        }
    }
}
