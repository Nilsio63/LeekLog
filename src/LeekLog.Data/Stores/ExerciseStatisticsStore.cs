using LeekLog.Abstractions.Entites;
using LeekLog.Abstractions.Models;
using LeekLog.Data.Abstractions.Stores;
using Microsoft.EntityFrameworkCore;

namespace LeekLog.Data.Stores;

public class ExerciseStatisticsStore : IExerciseStatisticsStore
{
    private readonly IDbContextFactory<LeekLogDbContext> _dbContextFactory;

    public ExerciseStatisticsStore(IDbContextFactory<LeekLogDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<List<ExerciseStatisticsModel>> GetAllByExerciseAsync(Guid userId, Guid exerciseId, CancellationToken ct = default)
    {
        using LeekLogDbContext context = await _dbContextFactory.CreateDbContextAsync(ct);

        return await context.Set<SessionExerciseEntity>()
            .Where(o => o.Session.UserId == userId)
            .Where(o => o.ExerciseId == exerciseId)
            .Select(o => new ExerciseStatisticsModel
            {
                Date = o.Session.Date,
                SetCount = o.Sets.Count(),
                MaxWeight = o.Sets.Max(o => o.UsedWeight),
                MaxRepetitions = o.Sets.Max(o => o.CleanRepetitions),
                TotalVolume = o.Sets.Sum(o => o.CleanRepetitions * o.UsedWeight)
            })
            .ToListAsync(ct);
    }
}
