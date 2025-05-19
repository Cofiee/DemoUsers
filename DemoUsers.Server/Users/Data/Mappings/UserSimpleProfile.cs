using AutoMapper;
using DemoUsers.Server.Users.Dtos;

namespace DemoUsers.Server.Users.Data.Mappings
{
    public class UserSimpleProfile : Profile
    {
        public UserSimpleProfile() 
        {
            CreateMap<UserEntity, UserSimple>()
                .ForMember(us => us.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(us => us.Name, opt => opt.MapFrom(src => src.Name));
        }   
    }
}
