using LeekLog.Abstractions.Entites;

namespace LeekLog.Services.Abstractions;

public interface IUserService
{
    Task<UserEntity?> TrySaveUserAsync(string userName, string password, CancellationToken ct = default);
}
