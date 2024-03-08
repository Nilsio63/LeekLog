using LeekLog.Abstractions.Entites;

namespace LeekLog.Data.Abstractions.Stores;

public interface IExerciseTagStore : IBaseStore<ExerciseTagEntity>
{
    Task<List<ExerciseTagEntity>> GetAllByExerciseIdAsync(Guid exerciseId, CancellationToken ct = default);
    Task<List<ExerciseTagEntity>> GetAllByTagIdsAsync(IEnumerable<Guid> tagIds, CancellationToken ct = default);
}
