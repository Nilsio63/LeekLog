using LeekLog.Abstractions.Entites;
using LeekLog.Data.Abstractions.Stores;
using Microsoft.EntityFrameworkCore;

namespace LeekLog.Data.Stores;

public class FeedbackStore : BaseStore<FeedbackEntity>, IFeedbackStore
{
    public FeedbackStore(IDbContextFactory<LeekLogDbContext> dbContextFactory)
        : base(dbContextFactory)
    {
    }

    public async Task<List<FeedbackEntity>> GetAllByUserIdAsync(string userId, CancellationToken ct = default)
    {
        if (Guid.TryParse(userId, out Guid userGuid) == false)
        {
            return new();
        }

        using LeekLogDbContext context = await _dbContextFactory.CreateDbContextAsync(ct);

        return await context
            .Set<FeedbackEntity>()
            .Where(o => o.UserId == userGuid)
            .ToListAsync(ct);
    }
}
