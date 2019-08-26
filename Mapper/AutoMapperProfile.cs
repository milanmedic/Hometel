using AutoMapper;
using Hometel.Domain.Models;
using Hometel.Domain.Models.Dto;

namespace Hometel.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<User, UserResponse>();
            CreateMap<Guest, UserDto>();
            CreateMap<UserDto, Guest>();
            CreateMap<Host, UserDto>();
            CreateMap<UserDto, Host>();
        }
    }
}