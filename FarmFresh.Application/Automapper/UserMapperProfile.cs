using AutoMapper;
using FarmFresh.Application.Dto.Request.Users;
using FarmFresh.Domain.Entities.Users;

namespace FarmFresh.Application.Automapper
{
    public class UserMapperProfile: Profile
    {
        public UserMapperProfile()
        {
            #region Users
            CreateMap<UserRequest, User>();
            #endregion Users
        }
    }
}
