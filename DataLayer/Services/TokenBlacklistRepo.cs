using Microsoft.EntityFrameworkCore;

namespace DataLayer;
public interface ITokenBlacklistRepository : IRepository<BlacklistedToken>
{
    Task<bool> IsTokenBlacklisted(string token); // Check if a token is blacklisted
    Task<List<BlacklistedToken>?> GetExpiredTokensAsync(); // Optional: Clean up expired tokens
}

public class TokenBlacklistRepo : Repository<BlacklistedToken>, ITokenBlacklistRepository
{
    private readonly IQueryable<BlacklistedToken> _queryable;


    public TokenBlacklistRepo(ApplicationDbContext context) : base(context)
    {
        _queryable = DbContext.Set<BlacklistedToken>();
    }

    public async Task<bool> IsTokenBlacklisted(string token)
    {
        try
        {
            return await _queryable.AnyAsync(x => x.Token == token && x.ExpiryDate > DateTime.UtcNow);
        }
        catch
        {
            return false;
        }
    }

    public async Task<List<BlacklistedToken>?> GetExpiredTokensAsync()
    {

        try
        {
            return await _queryable.Where(t => t.ExpiryDate <= DateTime.UtcNow).ToListAsync();

        }
        catch
        {
            return null;
        }
    }
}