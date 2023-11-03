using LeekLog.Abstractions.Entites;

namespace LeekLog.Services.Abstractions;

public interface IGymSessionService
{
    Task<GymSessionEntity?> GetByIdAsync(string id, CancellationToken ct = default);
    Task<List<GymSessionEntity>> GetAllByUserIdAsync(string userId, CancellationToken ct = default);
    Task SaveAsync(GymSessionEntity entity, CancellationToken ct = default);
    Task DeleteAsync(Guid sessionId, CancellationToken ct = default);
}
