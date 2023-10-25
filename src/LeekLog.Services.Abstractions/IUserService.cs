using LeekLog.Abstractions.Models;

namespace LeekLog.Services.Abstractions;

public interface IUserService
{
    Task<UserCreationResult> TrySaveUserAsync(string userName, string password, CancellationToken ct = default);
}
