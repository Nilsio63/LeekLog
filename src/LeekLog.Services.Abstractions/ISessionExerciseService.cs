using LeekLog.Abstractions.Entites;

namespace LeekLog.Services.Abstractions;

public interface ISessionExerciseService
{
    Task<List<SessionExerciseEntity>> GetAllByExerciseAsync(Guid userId, Guid exerciseId, CancellationToken ct = default);
}
