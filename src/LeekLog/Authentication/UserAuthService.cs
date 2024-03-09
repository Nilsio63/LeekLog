using Blazored.LocalStorage;
using LeekLog.Abstractions.Entites;
using LeekLog.Abstractions.Models;
using LeekLog.Services.Abstractions;

namespace LeekLog.Authentication;

public interface IUserAuthService
{
    Task<UserEntity?> GetCurrentUserAsync(CancellationToken ct = default);
    Task<bool> IsUserLoggedInAsync(CancellationToken ct = default);
    Task<string?> TryLoginAsync(string userName, string password, CancellationToken ct = default);
    Task LoginAsync(UserEntity user, CancellationToken ct = default);
    Task LogoutAsync(CancellationToken ct = default);
}

public class UserAuthService : IUserAuthService
{
    private readonly ILocalStorageService _localStorageService;
    private readonly ILeekLogAuthenticationStateProvider _authStateProvider;
    private readonly IUserService _userService;

    public UserAuthService(
        ILocalStorageService localStorageService,
        ILeekLogAuthenticationStateProvider authStateProvider,
        IUserService userService)
    {
        _localStorageService = localStorageService;
        _authStateProvider = authStateProvider;
        _userService = userService;
    }

    public async Task<bool> IsUserLoggedInAsync(CancellationToken ct = default)
    {
        string? userId = await _localStorageService.GetItemAsync<string>(AuthConstants.LocalStorageUserIdKey, ct);

        return !string.IsNullOrWhiteSpace(userId);
    }

    public async Task<UserEntity?> GetCurrentUserAsync(CancellationToken ct = default)
    {
        string? userId = await _localStorageService.GetItemAsync<string>(AuthConstants.LocalStorageUserIdKey, ct);

        if (string.IsNullOrWhiteSpace(userId))
        {
            return null;
        }

        UserEntity? user = await _userService.GetByIdAsync(userId, ct);

        return user;
    }

    public async Task<string?> TryLoginAsync(string userName, string password, CancellationToken ct = default)
    {
        UserLoginResult result = await _userService.TryGetByLoginAsync(userName, password, ct);

        if (result.Success == false)
        {
            return result.ErrorMessage;
        }

        await LoginAsync(result.User, ct);

        return null;
    }

    public async Task LoginAsync(UserEntity user, CancellationToken ct = default)
    {
        await _authStateProvider.SetAuthenticatedAsync(user, ct);
    }

    public async Task LogoutAsync(CancellationToken ct = default)
    {
        await _authStateProvider.RemoveAuthenticatedAsync(ct);
    }
}
