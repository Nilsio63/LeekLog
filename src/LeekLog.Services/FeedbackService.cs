using LeekLog.Abstractions.Entites;
using LeekLog.Data.Abstractions.Stores;
using LeekLog.Services.Abstractions;

namespace LeekLog.Services;

public class FeedbackService : IFeedbackService
{
    private readonly IFeedbackStore _feedbackStore;

    public FeedbackService(IFeedbackStore feedbackStore)
    {
        _feedbackStore = feedbackStore;
    }

    public async Task<List<FeedbackEntity>> GetAllByUserIdAsync(string userId, CancellationToken ct = default)
    {
        return await _feedbackStore.GetAllByUserIdAsync(userId, ct);
    }

    public async Task<FeedbackEntity?> GetByIdAsync(string id, CancellationToken ct = default)
    {
        return await _feedbackStore.GetByIdAsync(id, ct);
    }

    public async Task SaveAsync(FeedbackEntity entity, CancellationToken ct = default)
    {
        await _feedbackStore.SaveAsync(entity, ct);
    }

    public async Task DeleteAsync(Guid feedbackId, CancellationToken ct = default)
    {
        await _feedbackStore.DeleteAsync(feedbackId, ct);
    }
}
