using AutoMapper;
using PizzaApp.Domain.Entities;
using PizzaApp.DTOs.UserDTOs;

namespace PizzaApp.Mappers.MappersConfig
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, LoginUserDTO>().ReverseMap();
            CreateMap<User, RegisterUserDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}