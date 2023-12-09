namespace LeekLog.Abstractions.Entites;

public class UserEntity : BaseEntity
{
    public string UserName { get; set; } = string.Empty;
    public byte[] Password { get; set; } = Array.Empty<byte>();
    public bool IsAdmin { get; set; }
}
