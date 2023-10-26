using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace LeekLog.Services.Security;

public interface IPasswordEncoder
{
    byte[] EncodePassword(string password);
}

public class PasswordEncoder : IPasswordEncoder
{
    private readonly IOptions<SecuritySettings> _passwordSettings;

    public PasswordEncoder(IOptions<SecuritySettings> passwordSettings)
    {
        _passwordSettings = passwordSettings;
    }

    public byte[] EncodePassword(string password)
    {
        byte[] passwordHash = ComputeHash(password);
        byte[] saltHash = ComputeHash(_passwordSettings.Value.Salt);

        byte[] combinedHash = ComputeHash(passwordHash.Concat(saltHash).ToArray());

        return combinedHash;
    }

    private static byte[] ComputeHash(string text)
    {
        return ComputeHash(Encoding.UTF8.GetBytes(text));
    }

    private static byte[] ComputeHash(byte[] content)
    {
        using SHA256 hashAlgorithm = SHA256.Create();

        return hashAlgorithm.ComputeHash(content);
    }
}
