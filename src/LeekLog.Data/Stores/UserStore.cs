using LeekLog.Abstractions.Entites;
using LeekLog.Data.Abstractions.Stores;
using Microsoft.EntityFrameworkCore;

namespace LeekLog.Data.Stores;

public class UserStore : BaseStore<UserEntity>, IUserStore
{
    public UserStore(IDbContextFactory<LeekLogDbContext> dbContextFactory)
        : base(dbContextFactory)
    {
    }

    public async Task<UserEntity?> GetByLoginAsync(string login, CancellationToken ct = default)
    {
        using LeekLogDbContext context = await _dbContextFactory.CreateDbContextAsync(ct);

        return await context
            .Set<UserEntity>()
            .Where(o => o.UserName == login)
            .FirstOrDefaultAsync(ct);
    }
}
