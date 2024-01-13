using LeekLog.Abstractions.Entites;
using LeekLog.Data.Abstractions.Stores;
using LeekLog.Services.Abstractions;

namespace LeekLog.Services;

public class SessionExerciseService : ISessionExerciseService
{
    private readonly ISessionExerciseStore _store;

    public SessionExerciseService(ISessionExerciseStore store)
    {
        _store = store;
    }

    public async Task<List<SessionExerciseEntity>> GetAllByExerciseAsync(Guid userId, Guid exerciseId, CancellationToken ct = default)
    {
        return await _store.GetAllByExerciseAsync(userId, exerciseId, ct);
    }
}
