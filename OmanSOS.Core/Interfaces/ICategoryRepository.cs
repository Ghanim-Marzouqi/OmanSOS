using OmanSOS.Core.Models;

namespace OmanSOS.Core.Interfaces;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Task<int> GetNextId();
}
