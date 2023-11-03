using LeekLog.Abstractions.Entites;
using LeekLog.Data.Abstractions.Stores;
using LeekLog.Services.Abstractions;

namespace LeekLog.Services;

public class GymSessionService : IGymSessionService
{
    private readonly IGymSessionStore _gymSessionStore;

    public GymSessionService(IGymSessionStore gymSessionStore)
    {
        _gymSessionStore = gymSessionStore;
    }

    public async Task<GymSessionEntity?> GetByIdAsync(string id, CancellationToken ct = default)
    {
        return await _gymSessionStore.GetByIdAsync(id, ct);
    }

    public async Task<List<GymSessionEntity>> GetAllByUserIdAsync(string userId, CancellationToken ct = default)
    {
        return await _gymSessionStore.GetAllByUserIdAsync(userId, ct);
    }

    public async Task SaveAsync(GymSessionEntity entity, CancellationToken ct = default)
    {
        await _gymSessionStore.SaveAsync(entity, ct);
    }

    public async Task DeleteAsync(Guid sessionId, CancellationToken ct = default)
    {
        await _gymSessionStore.DeleteAsync(sessionId, ct);
    }
}
