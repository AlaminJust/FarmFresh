using FarmFresh.Application.Dto.Request.Users;
using FarmFresh.Application.Interfaces.Services.Users;
using FarmFresh.Domain.Entities.Users;
using FarmFresh.Domain.RepoInterfaces.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmFresh.Infrastructure.Service.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(
                IUserRepository userRepository
            )
        {
            _userRepository = userRepository;
        }
        public async Task AddAsync(UserRequest userRequest)
        {
            var user = new User
            {
                UserName = userRequest.UserName,
                FirstName = userRequest.FirstName,
                LastName = userRequest.LastName,
                Email = userRequest.Email,
                Password = userRequest.Password
            };
            
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
        }
    }
}
