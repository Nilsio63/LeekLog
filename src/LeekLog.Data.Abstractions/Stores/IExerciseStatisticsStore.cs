using LeekLog.Abstractions.Models;

namespace LeekLog.Data.Abstractions.Stores;

public interface IExerciseStatisticsStore
{
    Task<List<ExerciseStatisticsModel>> GetAllByExerciseAsync(Guid userId, Guid exerciseId, CancellationToken ct = default);
}
