using LeekLog.Abstractions.Entites;

namespace LeekLog.Data.Abstractions.Stores;

public interface IExerciseTagStore : IBaseStore<ExerciseTagEntity>
{
    Task<List<ExerciseTagEntity>> GetAllByTagIdsAsync(IEnumerable<Guid> tagIds, CancellationToken ct = default);
}
