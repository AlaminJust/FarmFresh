using FarmFresh.Domain.Entities.Users;

namespace FarmFresh.Domain.RepoInterfaces.Users
{
    public interface IRefreshTokenRepository : IBaseRepository<RefreshToken>
    {
        Task<RefreshToken> GetByTokenAsync(string refreshToken);
    }
}
