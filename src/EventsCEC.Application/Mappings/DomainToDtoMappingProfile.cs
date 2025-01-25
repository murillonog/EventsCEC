using AutoMapper;
using EventsCEC.Application.Dtos;
using EventsCEC.Application.ViewModels;
using EventsCEC.Domain.Identity;

namespace EventsCEC.Application.Mappings;

public class DomainToDtoMappingProfile : Profile
{
    public DomainToDtoMappingProfile()
    {
        CreateMap<RegisterViewModel, UserRegisterDto>();

        CreateMap<ApplicationUser, UserRegisterDto>().ReverseMap();

        CreateMap<UserRegisterDto, ApplicationUser>()
            .ForMember(r => r.UserName, u => u.MapFrom(x => x.Email))
            .ForMember(r => r.NormalizedUserName, u => u.MapFrom(x => x.Email.ToUpper()))
            .ForMember(r => r.PhoneNumber, u => u.MapFrom(x => x.Phone));
    }
}
