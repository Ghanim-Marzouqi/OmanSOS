using OmanSOS.Core.Models;

namespace OmanSOS.Core.Interfaces;

public interface IDonationRepository : IBaseRepository<Donation>
{
    Task<IEnumerable<Donation>> GetDonationsByUserId(int userId);

    Task<int> AddCampaign(Campaign campaign);

    Task<IEnumerable<Campaign>> GetCampaigns();
}
