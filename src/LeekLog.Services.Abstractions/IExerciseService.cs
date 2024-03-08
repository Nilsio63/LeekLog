using LeekLog.Abstractions.Entites;
using LeekLog.Abstractions.Models;

namespace LeekLog.Services.Abstractions;

public interface IExerciseService
{
    Task<ExerciseEntity?> GetByIdAsync(string id, CancellationToken ct = default);
    Task<List<ExerciseEntity>> GetAllAsync(CancellationToken ct = default);
    Task<IEnumerable<ExerciseEntity>> SearchAsync(string searchText, IEnumerable<ExerciseEntity>? searchList = null, CancellationToken ct = default);
    Task SaveAsync(ExerciseEditModel exerciseEditModel, CancellationToken ct = default);
}
