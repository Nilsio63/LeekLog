using LeekLog.Abstractions.Entites;
using LeekLog.Data.Abstractions.Stores;
using Microsoft.EntityFrameworkCore;

namespace LeekLog.Data.Stores;

public abstract class BaseStore<T> : IBaseStore<T> where T : BaseEntity
{
    protected readonly IDbContextFactory<LeekLogDbContext> _dbContextFactory;

    public BaseStore(IDbContextFactory<LeekLogDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<List<T>> GetAllAsync(CancellationToken ct = default)
    {
        using LeekLogDbContext context = await _dbContextFactory.CreateDbContextAsync(ct);

        return await context.Set<T>().ToListAsync(ct);
    }

    public virtual async Task<T?> GetByIdAsync(string id, CancellationToken ct = default)
    {
        if (Guid.TryParse(id, out Guid guid) == false)
        {
            return null;
        }

        using LeekLogDbContext context = await _dbContextFactory.CreateDbContextAsync(ct);

        return await context
            .Set<T>()
            .Where(o => o.Id == guid)
            .FirstOrDefaultAsync(ct);
    }

    public async Task SaveAsync(T entity, CancellationToken ct = default)
    {
        await SaveAllAsync(new[] { entity }, ct);
    }

    public async Task SaveAllAsync(IEnumerable<T> entities, CancellationToken ct = default)
    {
        entities = entities.ToArray();

        if (entities.Any() == false)
        {
            return;
        }

        using LeekLogDbContext context = await _dbContextFactory.CreateDbContextAsync(ct);

        foreach (T entity in entities)
        {
            if (await context.Set<T>().AnyAsync(o => o.Id == entity.Id, ct))
            {
                context.Update(entity);
            }
            else
            {
                context.Add(entity);
            }
        }

        await context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(T entity, CancellationToken ct = default)
    {
        await DeleteAllAsync(new[] { entity }, ct);
    }

    public async Task DeleteAllAsync(IEnumerable<T> entities, CancellationToken ct = default)
    {
        entities = entities.ToArray();

        if (entities.Any() == false)
        {
            return;
        }

        using LeekLogDbContext context = await _dbContextFactory.CreateDbContextAsync(ct);

        context.RemoveRange(entities);

        await context.SaveChangesAsync(ct);
    }
}
