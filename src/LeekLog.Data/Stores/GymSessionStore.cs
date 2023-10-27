using LeekLog.Abstractions.Entites;
using LeekLog.Data.Abstractions.Stores;
using Microsoft.EntityFrameworkCore;

namespace LeekLog.Data.Stores;

public class GymSessionStore : BaseStore<GymSessionEntity>, IGymSessionStore
{
    public GymSessionStore(IDbContextFactory<LeekLogDbContext> dbContextFactory)
        : base(dbContextFactory)
    {
    }

    public async Task<List<GymSessionEntity>> GetAllByUserIdAsync(string userId, CancellationToken ct = default)
    {
        if (Guid.TryParse(userId, out Guid userGuid) == false)
        {
            return new();
        }

        using LeekLogDbContext dbContext = await _dbContextFactory.CreateDbContextAsync(ct);

        return await dbContext
            .Set<GymSessionEntity>()
            .Where(o => o.UserId == userGuid)
            .ToListAsync(ct);
    }
}
