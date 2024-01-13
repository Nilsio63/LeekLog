using LeekLog.Abstractions.Entites;

namespace LeekLog.Data.Abstractions.Stores;

public interface ISessionExerciseStore : IBaseStore<SessionExerciseEntity>
{
    Task<List<SessionExerciseEntity>> GetAllByExerciseAsync(Guid userId, Guid exerciseId, CancellationToken ct = default);
}
