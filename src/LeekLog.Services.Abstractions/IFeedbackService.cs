using LeekLog.Abstractions.Entites;

namespace LeekLog.Services.Abstractions;

public interface IFeedbackService
{
    Task<FeedbackEntity?> GetByIdAsync(string id, CancellationToken ct = default);
    Task<List<FeedbackEntity>> GetAllByUserIdAsync(string userId, CancellationToken ct = default);
    Task SaveAsync(FeedbackEntity entity, CancellationToken ct = default);
}
