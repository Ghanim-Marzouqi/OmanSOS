using OmanSOS.Core.Models;

namespace OmanSOS.Core.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByEmailAsync(string? email);

    Task<User?> GetByNationalIdAsync(int nationalId);
}
