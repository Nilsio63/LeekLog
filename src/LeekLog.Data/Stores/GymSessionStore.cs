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

    public override async Task<GymSessionEntity?> GetByIdAsync(string id, CancellationToken ct = default)
    {
        if (Guid.TryParse(id, out Guid guid) == false)
        {
            return null;
        }

        using LeekLogDbContext context = await _dbContextFactory.CreateDbContextAsync(ct);

        return await context
            .Set<GymSessionEntity>()
            .Where(o => o.Id == guid)
            .Include(o => o.Exercises)
                .ThenInclude(o => o.Exercise)
            .Include(o => o.Exercises)
                .ThenInclude(o => o.Sets)
            .FirstOrDefaultAsync(ct);
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
            .Include(o => o.Exercises)
                .ThenInclude(o => o.Exercise)
            .Include(o => o.Exercises)
                .ThenInclude(o => o.Sets)
            .ToListAsync(ct);
    }
}
