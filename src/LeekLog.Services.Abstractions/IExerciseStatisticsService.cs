using LeekLog.Abstractions.Models;

namespace LeekLog.Services.Abstractions;

public interface IExerciseStatisticsService
{
    Task<List<ExerciseStatisticsModel>> GetAllByExerciseAsync(Guid userId, Guid exerciseId, CancellationToken ct = default);
}
