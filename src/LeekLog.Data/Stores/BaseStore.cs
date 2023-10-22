using LeekLog.Abstractions.Entites;
using Microsoft.EntityFrameworkCore;

namespace LeekLog.Data.Stores;

public interface IBaseStore<T>
    where T : BaseEntity
{
    Task<T?> LoadByIdAsync(string id, CancellationToken ct = default);
    Task SaveAsync(T entity, CancellationToken ct = default);
}

public abstract class BaseStore<T> : IBaseStore<T> where T : BaseEntity
{
    protected readonly IDbContextFactory<LeekLogDbContext> _dbContextFactory;

    public BaseStore(IDbContextFactory<LeekLogDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<T?> LoadByIdAsync(string id, CancellationToken ct = default)
    {
        Guid guid = Guid.Parse(id);

        using LeekLogDbContext context = await _dbContextFactory.CreateDbContextAsync(ct);

        return await context
            .Set<T>()
            .Where(o => o.Id == guid)
            .FirstOrDefaultAsync(ct);
    }

    public async Task SaveAsync(T entity, CancellationToken ct = default)
    {
        using LeekLogDbContext context = await _dbContextFactory.CreateDbContextAsync(ct);

        if (await context.Set<T>().AnyAsync(o => o.Id == entity.Id, ct))
        {
            context.Update(entity);
        }
        else
        {
            context.Add(entity);
        }

        await context.SaveChangesAsync(ct);
    }
}
