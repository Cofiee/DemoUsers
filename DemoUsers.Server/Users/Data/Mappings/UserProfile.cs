using AutoMapper;
using DemoUsers.Server.Users.Dtos;

namespace DemoUsers.Server.Users.Data.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<UserEntity, User>()
                .ForMember(u => u.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(u => u.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(u => u.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(u => u.Image, opt => opt.MapFrom(src => src.Image))
                .ReverseMap();
        }
    }
}
