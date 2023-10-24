using LeekLog.Abstractions.Entites;

namespace LeekLog.Data.Abstractions.Stores;

public interface IUserStore : IBaseStore<UserEntity>
{
    Task<UserEntity?> GetByLoginAsync(string login, CancellationToken ct = default);
}
