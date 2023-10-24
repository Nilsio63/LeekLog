using System.Security.Cryptography;
using System.Text;

namespace LeekLog.Services.Security;

public interface IPasswordEncoder
{
    byte[] EncodePassword(string password);
}

public class PasswordEncoder : IPasswordEncoder
{
    public byte[] EncodePassword(string password)
    {
        using SHA256 hashAlgorithm = SHA256.Create();

        return hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(password));
    }
}
