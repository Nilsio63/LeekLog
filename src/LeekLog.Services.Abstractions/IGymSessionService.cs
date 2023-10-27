using LeekLog.Abstractions.Entites;

namespace LeekLog.Services.Abstractions;

public interface IGymSessionService
{
    Task<List<GymSessionEntity>> GetAllByUserIdAsync(string userId, CancellationToken ct = default);
}
