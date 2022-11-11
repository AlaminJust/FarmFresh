using AutoMapper;
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
        private readonly IMapper _mapper;

        public UserService(
                IUserRepository userRepository,
                IMapper mapper
            )
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task AddAsync(UserRequest userRequest)
        {
            var user = _mapper.Map<User>(userRequest);
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
        }
    }
}
