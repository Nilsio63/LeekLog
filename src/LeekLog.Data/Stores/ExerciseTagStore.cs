using LeekLog.Abstractions.Entites;
using LeekLog.Data.Abstractions.Stores;
using Microsoft.EntityFrameworkCore;

namespace LeekLog.Data.Stores;

public class ExerciseTagStore : BaseStore<ExerciseTagEntity>, IExerciseTagStore
{
    public ExerciseTagStore(IDbContextFactory<LeekLogDbContext> dbContextFactory)
        : base(dbContextFactory)
    {
    }

    public async Task<List<ExerciseTagEntity>> GetAllByTagIdsAsync(IEnumerable<Guid> tagIds, CancellationToken ct = default)
    {
        tagIds = tagIds.Distinct().ToArray();

        if (tagIds.Any() == false)
        {
            return new();
        }

        using LeekLogDbContext context = await _dbContextFactory.CreateDbContextAsync(ct);

        return await context
            .Set<ExerciseTagEntity>()
            .Where(o => tagIds.Contains(o.TagId))
            .ToListAsync(ct);
    }
}
