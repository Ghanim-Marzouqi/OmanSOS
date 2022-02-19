using OmanSOS.Core.Interfaces;

namespace OmanSOS.Core;

public interface IUnitOfWork
{
    ICategoryRepository Categories { get; }
    IDonationRepository Donations { get; }
    ILocationRepository Locations { get; }
    IPriorityRepository Priorities { get; }
    IRequestRepository Requests { get; }
    IUserRepository Users { get; }
    IUserTypeRepository UserTypes { get; }
}
