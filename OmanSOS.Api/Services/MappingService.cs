using AutoMapper;
using OmanSOS.Core.Models;
using OmanSOS.Core.ViewModels;

namespace OmanSOS.Api.Services;

public class MappingService : Profile
{
    public MappingService()
    {
        CreateMap<Category, CategoryViewModel>().ReverseMap();
        CreateMap<Donation, DonationViewModel>().ReverseMap();
        CreateMap<Location, LocationViewModel>().ReverseMap();
        CreateMap<Request, RequestViewModel>().ReverseMap();
        CreateMap<Priority, PriorityViewModel>().ReverseMap();
        CreateMap<User, UserViewModel>().ReverseMap();
        CreateMap<UserType, UserTypeViewModel>().ReverseMap();
    }
}
