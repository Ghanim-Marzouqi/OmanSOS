using Microsoft.Extensions.Configuration;
using OmanSOS.Core;
using OmanSOS.Core.Interfaces;
using OmanSOS.Data.Repositories;

namespace OmanSOS.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IConfiguration configuration)
        {
            Categories = new CategoryRepository(configuration);
            Donations = new DonationRepository(configuration);
            Priorities = new PriorityRepository(configuration);
            Requests = new RequestRepository(configuration);
            Users = new UserRepository(configuration);
            UserTypes = new UserTypeRepository(configuration);
        }

        public ICategoryRepository Categories { get; }

        public IDonationRepository Donations { get; }

        public IPriorityRepository Priorities { get; }

        public IRequestRepository Requests { get; }

        public IUserRepository Users { get; }

        public IUserTypeRepository UserTypes { get; }
    }
}
