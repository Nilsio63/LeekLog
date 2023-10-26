using LeekLog.Abstractions.Entites;
using LeekLog.Abstractions.Models;
using LeekLog.Data.Abstractions.Stores;
using LeekLog.Services.Abstractions;
using LeekLog.Services.Security;

namespace LeekLog.Services;

public class UserService : IUserService
{
    private readonly IUserStore _userStore;
    private readonly IPasswordEncoder _passwordEncoder;

    public UserService(
        IUserStore userStore,
        IPasswordEncoder passwordEncoder)
    {
        _userStore = userStore;
        _passwordEncoder = passwordEncoder;
    }

    public async Task<UserEntity?> GetByIdAsync(string userId, CancellationToken ct = default)
    {
        return await _userStore.GetByIdAsync(userId, ct);
    }

    public async Task<UserEntity?> GetByLoginAsync(string userName, CancellationToken ct = default)
    {
        return await _userStore.GetByLoginAsync(userName, ct);
    }

    public async Task<UserCreationResult> TrySaveUserAsync(string userName, string password, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(userName))
        {
            return new UserCreationResult("Username is required");
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            return new UserCreationResult("Password is required");
        }

        UserEntity? existingUser = await _userStore.GetByLoginAsync(userName, ct);

        if (existingUser is not null)
        {
            return new UserCreationResult($"User {existingUser.UserName} already exists");
        }

        byte[] encodedPassword = _passwordEncoder.EncodePassword(password);

        UserEntity newUser = new()
        {
            UserName = userName,
            Password = encodedPassword
        };

        await _userStore.SaveAsync(newUser, ct);

        return new UserCreationResult(newUser);
    }
}
