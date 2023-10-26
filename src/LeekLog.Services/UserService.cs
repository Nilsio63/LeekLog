﻿using LeekLog.Abstractions.Entites;
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

    public async Task<UserLoginResult> TryGetByLoginAsync(string userName, string password, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(userName))
        {
            return new UserLoginResult("User name is required");
        }
        else if (string.IsNullOrWhiteSpace(password))
        {
            return new UserLoginResult("Password is required");
        }

        UserEntity? user = await GetByLoginAsync(userName, ct);

        if (user == null)
        {
            return new UserLoginResult($"User name {userName} does not exist");
        }

        byte[] passwordHash = _passwordEncoder.EncodePassword(password);

        if (passwordHash.SequenceEqual(user.Password) == false)
        {
            return new UserLoginResult("Wrong password");
        }

        return new UserLoginResult(user);
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
