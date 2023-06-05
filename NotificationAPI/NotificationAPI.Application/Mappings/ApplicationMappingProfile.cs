using AutoMapper;
using NotificationAPI.Application.Abstraction.Models;
using NotificationAPI.Domain;

namespace NotificationAPI.Application.Mappings
{
    public class ApplicationMappingProfile : Profile
    {
        protected ApplicationMappingProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
