using OmanSOS.Core.Models;

namespace OmanSOS.Core.Interfaces;

public interface ILocationRepository : IBaseRepository<Location>
{
    Task<int> GetNextId();
}
