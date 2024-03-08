using LeekLog.Abstractions.Entites;
using LeekLog.Data.Abstractions.Stores;
using Microsoft.EntityFrameworkCore;

namespace LeekLog.Data.Stores;

public class ExerciseStore : BaseStore<ExerciseEntity>, IExerciseStore
{
    public ExerciseStore(IDbContextFactory<LeekLogDbContext> dbContextFactory)
        : base(dbContextFactory)
    {
    }

    public override async Task<ExerciseEntity?> GetByIdAsync(string id, CancellationToken ct = default)
    {
        if (Guid.TryParse(id, out Guid guid) == false)
        {
            return null;
        }

        using LeekLogDbContext context = await _dbContextFactory.CreateDbContextAsync(ct);

        return await context
            .Set<ExerciseEntity>()
            .Where(o => o.Id == guid)
            .Include(o => o.ExerciseTags)
            .ThenInclude(o => o.Tag)
            .FirstOrDefaultAsync(ct);
    }

    public override async Task<List<ExerciseEntity>> GetAllAsync(CancellationToken ct = default)
    {
        using LeekLogDbContext context = await _dbContextFactory.CreateDbContextAsync(ct);

        return await context.Set<ExerciseEntity>()
            .Include(o => o.ExerciseTags)
            .ThenInclude(o => o.Tag)
            .ToListAsync(ct);
    }
}
