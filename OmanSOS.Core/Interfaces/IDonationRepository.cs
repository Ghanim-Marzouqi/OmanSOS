using OmanSOS.Core.Models;

namespace OmanSOS.Core.Interfaces;

public interface IDonationRepository : IBaseRepository<Donation>
{
    Task<IEnumerable<Donation>> GetDonationsByUserId(int userId);
}
