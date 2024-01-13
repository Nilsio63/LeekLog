using LeekLog.Abstractions.Entites;
using LeekLog.Data.Abstractions.Stores;
using Microsoft.EntityFrameworkCore;

namespace LeekLog.Data.Stores;

public class SessionExerciseStore : BaseStore<SessionExerciseEntity>, ISessionExerciseStore
{
    public SessionExerciseStore(IDbContextFactory<LeekLogDbContext> dbContextFactory)
        : base(dbContextFactory)
    {
    }

    public async Task<List<SessionExerciseEntity>> GetAllByExerciseAsync(Guid userId, Guid exerciseId, CancellationToken ct = default)
    {
        using LeekLogDbContext context = await _dbContextFactory.CreateDbContextAsync(ct);

        return await context
            .Set<SessionExerciseEntity>()
            .Where(o => o.Session.UserId == userId)
            .Where(o => o.ExerciseId == exerciseId)
            .OrderByDescending(o => o.Session.Date)
            .Include(o => o.Session)
            .Include(o => o.Sets)
            .ToListAsync(ct);
    }
}
