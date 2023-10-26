using Blazored.LocalStorage;
using LeekLog.Abstractions.Entites;
using LeekLog.Services.Abstractions;
using LeekLog.Services.Security;

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
    private readonly IPasswordEncoder _passwordEncoder;

    public UserAuthService(
        ILocalStorageService localStorageService,
        ILeekLogAuthenticationStateProvider authStateProvider,
        IUserService userService,
        IPasswordEncoder passwordEncoder)
    {
        _localStorageService = localStorageService;
        _authStateProvider = authStateProvider;
        _userService = userService;
        _passwordEncoder = passwordEncoder;
    }

    public async Task<bool> IsUserLoggedInAsync(CancellationToken ct = default)
    {
        string userId = await _localStorageService.GetItemAsync<string>(AuthConstants.LocalStorageUserIdKey, ct);

        return !string.IsNullOrWhiteSpace(userId);
    }

    public async Task<UserEntity?> GetCurrentUserAsync(CancellationToken ct = default)
    {
        string userId = await _localStorageService.GetItemAsync<string>(AuthConstants.LocalStorageUserIdKey, ct);

        if (string.IsNullOrWhiteSpace(userId))
        {
            return null;
        }

        UserEntity? user = await _userService.GetByIdAsync(userId, ct);

        return user;
    }

    public async Task<string?> TryLoginAsync(string userName, string password, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(userName))
        {
            return "User name is required";
        }
        else if (string.IsNullOrWhiteSpace(password))
        {
            return "Password is required";
        }

        UserEntity? user = await _userService.GetByLoginAsync(userName, ct);

        if (user == null)
        {
            return $"User name {userName} does not exist";
        }

        byte[] passwordHash = _passwordEncoder.EncodePassword(password);

        if (passwordHash.SequenceEqual(user.Password) == false)
        {
            return "Wrong password";
        }

        await LoginAsync(user, ct);

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
