using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmFresh.Application.Dto.Response.Users
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string Token { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string FullName
        {
            get
            {
                return FirstName + ' ' + LastName;
            }
        }
    }
}
