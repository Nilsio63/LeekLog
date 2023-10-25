using LeekLog.Abstractions.Entites;

namespace LeekLog.Data.Abstractions.Stores;

public interface IBaseStore<T>
    where T : BaseEntity
{
    Task<List<T>> GetAllAsync(CancellationToken ct = default);
    Task<T?> GetByIdAsync(string id, CancellationToken ct = default);
    Task SaveAsync(T entity, CancellationToken ct = default);
    Task SaveAllAsync(IEnumerable<T> entities, CancellationToken ct = default);
}
