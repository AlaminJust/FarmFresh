using AutoMapper;
using FarmFresh.Application.Dto.Request.Users;
using FarmFresh.Application.Dto.Response.Users;
using FarmFresh.Application.Interfaces.Services.Users;
using FarmFresh.Core.Enums;
using FarmFresh.Domain.Entities.Users;
using FarmFresh.Domain.RepoInterfaces;
using FarmFresh.Domain.RepoInterfaces.Users;
using BC = BCrypt.Net.BCrypt;

namespace FarmFresh.Infrastructure.Service.Services.Users
{
    public class UserService : IUserService
    {
        #region Properties
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ITransactionUtil _transaction;
        #endregion Properties

        #region Constructor
        public UserService(
                IUserRepository userRepository,
                IMapper mapper,
                IUserRoleRepository userRoleRepository,
                IRoleRepository roleRepository,
                ITransactionUtil transaction
            )
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _transaction = transaction;
        }

        #endregion Constructor

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
            await _transaction.BeginAsync();
            try
            {
                var role = await _roleRepository.GetByTypeAsync(RoleType.Customer);
                var user = _mapper.Map<User>(userRequest);

                await _userRepository.AddAsync(user);
                await _userRepository.SaveChangesAsync();

                await _userRoleRepository.AddAsync(new UserRole
                {
                    UserId = user.Id,
                    RoleId = role.Id,
                    IsActive = true
                });
                
                await _userRoleRepository.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            catch (Exception)
            {
                await _transaction.RollBackAsync();
                throw;
            }
        }
        #endregion Save
    }
}
