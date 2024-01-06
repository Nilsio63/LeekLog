using LeekLog.Abstractions.Entites;

namespace LeekLog.Services.Abstractions;

public interface IExerciseService
{
    Task<ExerciseEntity?> GetByIdAsync(string id, CancellationToken ct = default);
    Task<List<ExerciseEntity>> GetAllAsync(CancellationToken ct = default);
    Task SaveAsync(ExerciseEntity exercise, CancellationToken ct = default);
}
