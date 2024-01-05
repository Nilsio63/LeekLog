using LeekLog.Abstractions.Models;
using LeekLog.Data.Abstractions.Stores;
using LeekLog.Services.Abstractions;

namespace LeekLog.Services;

public class ExerciseStatisticsService : IExerciseStatisticsService
{
    private readonly IExerciseStatisticsStore _statisticsStore;

    public ExerciseStatisticsService(IExerciseStatisticsStore statisticsStore)
    {
        _statisticsStore = statisticsStore;
    }

    public async Task<List<ExerciseStatisticsModel>> GetAllByExerciseAsync(Guid userId, Guid exerciseId, CancellationToken ct = default)
    {
        return await _statisticsStore.GetAllByExerciseAsync(userId, exerciseId, ct);
    }
}
