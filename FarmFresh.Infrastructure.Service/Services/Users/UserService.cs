using AutoMapper;
using FarmFresh.Application.Dto.Request.Users;
using FarmFresh.Application.Dto.Response.Users;
using FarmFresh.Application.Enums;
using FarmFresh.Application.Interfaces.Services.Users;
using FarmFresh.Domain.Dto.Request.Users;
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
        
        public async Task<UserResponse> FindByIdAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return _mapper.Map<UserResponse>(user);
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

        #region Update
        public async Task UpdateAsync(int userId, UserAddressRequest userRequest)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user is null)
            {
                throw new Exception("User not found");
            }

            user.Email = userRequest.Email ?? user.Email;
            user.FirstName = userRequest.FirstName ?? user.FirstName;
            user.LastName = userRequest.LastName ?? user.LastName;
            user.PhoneNumber = userRequest.PhoneNumber ?? user.PhoneNumber;
            user.BillingAddress = userRequest.BillingAddress ?? user.BillingAddress;
            user.ShippingAddress = userRequest.ShippingAddress ?? user.ShippingAddress;

            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();
        }
        #endregion Update

        #region Delete
        public async Task DeleteAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            
            if (user is null)
            {
                throw new Exception("User not found");
            }
            
            user.IsDeleted = true;
            user.UpdatedOn = DateTime.UtcNow;
            
            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();
        }
        #endregion Delete
    }
}