using LeekLog.Abstractions.Entites;

namespace LeekLog.Data.Abstractions.Stores;

public interface IGymSessionStore : IBaseStore<GymSessionEntity>
{
    Task<List<GymSessionEntity>> GetAllByUserIdAsync(string userId, CancellationToken ct = default);
}
