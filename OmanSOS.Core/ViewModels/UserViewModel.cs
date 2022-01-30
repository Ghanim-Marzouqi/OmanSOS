namespace OmanSOS.Core.ViewModels;

public class UserViewModel : BaseViewModel
{
    // Main Properties
    public int UserTypeId { get; set; } = 1;
    public int NationalId { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Location { get; set; }
    public string? Password { get; set; }
    public byte[]? PasswordHash { get; set; } = null;
    public byte[]? PasswordSalt { get; set; } = null;
    public string? AccessToken { get; set; } = null;

    // Navigation Properties
    public UserTypeViewModel? UserType { get; set; } = null;
}
