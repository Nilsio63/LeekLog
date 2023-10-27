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

    public async Task<List<GymSessionEntity>> GetAllByUserIdAsync(string userId, CancellationToken ct = default)
    {
        return await _gymSessionStore.GetAllByUserIdAsync(userId, ct);
    }
}
