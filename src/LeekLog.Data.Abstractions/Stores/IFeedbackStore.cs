using LeekLog.Abstractions.Entites;

namespace LeekLog.Data.Abstractions.Stores;

public interface IFeedbackStore : IBaseStore<FeedbackEntity>
{
    Task<List<FeedbackEntity>> GetAllByUserIdAsync(string userId, CancellationToken ct = default);
}
