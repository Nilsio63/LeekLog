using LeekLog.Abstractions.Entites;

namespace LeekLog.Services.Abstractions;

public interface IExerciseService
{
    Task<List<ExerciseEntity>> GetAllAsync(CancellationToken ct = default);
}
