using FarmFresh.Domain.Entities.Users;
using FarmFresh.Domain.RepoInterfaces.Users;
using FarmFresh.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace FarmFresh.Infrastructure.Repo.Repositories.Users
{
    public class RefreshTokenRepository : BaseRepository<RefreshToken>, IRefreshTokenRepository
    {
        private readonly EFDbContext _context;

        public RefreshTokenRepository(EFDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<RefreshToken> GetByTokenAsync(string refreshToken)
        {
            return await _context.RefreshTokens.Where(x => x.ReplacedByToken == refreshToken)?.FirstOrDefaultAsync();
        }
    }
}
