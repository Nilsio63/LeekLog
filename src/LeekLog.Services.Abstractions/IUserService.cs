using LeekLog.Abstractions.Entites;
using LeekLog.Abstractions.Models;

namespace LeekLog.Services.Abstractions;

public interface IUserService
{
    Task<UserEntity?> GetByIdAsync(string userId, CancellationToken ct = default);
    Task<UserEntity?> GetByLoginAsync(string userName, CancellationToken ct = default);
    Task<UserCreationResult> TrySaveUserAsync(string userName, string password, CancellationToken ct = default);
}
