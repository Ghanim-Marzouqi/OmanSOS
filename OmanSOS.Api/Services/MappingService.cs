using AutoMapper;
using OmanSOS.Core.Models;
using OmanSOS.Core.ViewModels;

namespace OmanSOS.Api.Services;

public class MappingService : Profile
{
    public MappingService()
    {
        CreateMap<User, UserViewModel>().ReverseMap();
        CreateMap<UserType, UserTypeViewModel>().ReverseMap();
        CreateMap<Request, RequestViewModel>().ReverseMap();
        CreateMap<Category, CategoryViewModel>().ReverseMap();
        CreateMap<Priority, PriorityViewModel>().ReverseMap();
        CreateMap<Donation, DonationViewModel>().ReverseMap();
    }
}
