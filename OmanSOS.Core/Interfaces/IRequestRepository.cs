using OmanSOS.Core.Models;

namespace OmanSOS.Core.Interfaces;

public interface IRequestRepository : IBaseRepository<Request>
{
    Task<Category> GetCategoryByRequestIdAsync(int requestId);
    Task<Location> GetLocationByRequestIdAsync(int requestId);
    Task<Priority> GetPriorityByRequestIdAsync(int requestId);
    Task<User> GetRequestorByRequestIdAsync(int requestId);
    Task<IEnumerable<Request>> GetRequestsByUserIdAsync(int userId);
    Task<bool> IsOpenRequestExisted(int userId);
}
