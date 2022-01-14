using AutoMapper;
using OmanSOS.Core.Models;
using OmanSOS.Core.ViewModels;

namespace OmanSOS.Api.Services
{
    public class MappingService : Profile
    {
        public MappingService()
        {
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<UserType, UserTypeViewModel>().ReverseMap();
        }
    }
}
