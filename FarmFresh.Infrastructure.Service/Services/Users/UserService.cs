using AutoMapper;
using FarmFresh.Application.Dto.Request.Users;
using FarmFresh.Application.Dto.Response.Users;
using FarmFresh.Application.Interfaces.Services.Users;
using FarmFresh.Domain.Entities.Users;
using FarmFresh.Domain.RepoInterfaces.Users;
using BC = BCrypt.Net.BCrypt;

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

        #region Get
        public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _userRepository.GetByUserNameAsync(loginRequest.UserName);
            if (user is null)
            {
                throw new Exception("User not found");
            }

            if (!BC.Verify(loginRequest.Password, user.Password))
            {
                throw new Exception("Password is incorrect");
            }
            var loginResponse = _mapper.Map<LoginResponse>(user);
            
            return loginResponse;
        }

        #endregion Get


        #region Save
        public async Task AddAsync(UserRequest userRequest)
        {
            var user = _mapper.Map<User>(userRequest);
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
        }
        #endregion Save

    }
}
