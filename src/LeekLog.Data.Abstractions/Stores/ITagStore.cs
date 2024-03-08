using LeekLog.Abstractions.Entites;

namespace LeekLog.Data.Abstractions.Stores;

public interface ITagStore : IBaseStore<TagEntity>
{
    Task<List<TagEntity>> GetAllByTitlesAsync(IEnumerable<string> tagTitles, CancellationToken ct = default);
}
