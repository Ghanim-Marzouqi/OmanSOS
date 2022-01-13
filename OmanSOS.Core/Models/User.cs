namespace OmanSOS.Core.Models
{
    public class User : Base
    {
        // Base Properties
        public int UserTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }

        // Navigation Properties
        public UserType? UserType { get; set; }
    }
}
