using OmanSOS.Core.ViewModels;

namespace OmanSOS.Api.Interfaces;

public interface IAuthService
{
    public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);

    public bool VerifyPasswordHash(string? password, byte[]? passwordHash, byte[]? passwordSalt);

    public string CreateAccessToken(UserViewModel user);
}
