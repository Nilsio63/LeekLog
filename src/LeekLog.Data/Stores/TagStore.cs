using LeekLog.Abstractions.Entites;
using LeekLog.Data.Abstractions.Stores;
using Microsoft.EntityFrameworkCore;

namespace LeekLog.Data.Stores;

public class TagStore : BaseStore<TagEntity>, ITagStore
{
    public TagStore(IDbContextFactory<LeekLogDbContext> dbContextFactory)
        : base(dbContextFactory)
    {
    }

    public async Task<List<TagEntity>> GetAllByTitlesAsync(IEnumerable<string> tagTitles, CancellationToken ct = default)
    {
        tagTitles = tagTitles
            .Select(o => o.ToLower())
            .Distinct()
            .ToArray();

        if (tagTitles.Any() == false)
        {
            return new();
        }

        using LeekLogDbContext context = await _dbContextFactory.CreateDbContextAsync(ct);

        return await context
            .Set<TagEntity>()
            .Where(o => tagTitles.Contains(o.Title.ToLower()))
            .ToListAsync(ct);
    }
}
