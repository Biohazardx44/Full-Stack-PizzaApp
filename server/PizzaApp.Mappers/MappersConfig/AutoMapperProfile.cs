using AutoMapper;
using PizzaApp.Domain.Entities;
using PizzaApp.DTOs.OrderDTOs;
using PizzaApp.DTOs.PizzaDTOs;
using PizzaApp.DTOs.UserDTOs;

namespace PizzaApp.Mappers.MappersConfig
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // User Mapping
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, RegisterUserDTO>().ReverseMap();
            CreateMap<User, LoginUserDTO>().ReverseMap();

            // Pizza Mapping
            CreateMap<Pizza, PizzaDTO>().ReverseMap();
            CreateMap<Pizza, AddPizzaDTO>().ReverseMap();
            CreateMap<Pizza, UpdatePizzaDTO>().ReverseMap();

            // Order Mapping
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Order, AddOrderDTO>().ReverseMap();
            CreateMap<Order, UpdateOrderDTO>().ReverseMap();
        }
    }
}