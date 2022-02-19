namespace OmanSOS.Core.Models;

public class User : Base
{
    public int UserTypeId { get; set; }
    public int NationalId { get; set; }
    public int LocationId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }
}
