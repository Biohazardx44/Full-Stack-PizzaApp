using AutoMapper;
using PizzaApp.DataAccess.Repositories.Abstraction;
using PizzaApp.Domain.Entities;
using PizzaApp.DTOs.OrderDTOs;
using PizzaApp.Services.Abstraction;
using PizzaApp.Shared.Responses;

namespace PizzaApp.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<Response<OrderDTO>> CreateOrderAsync(string userId, AddOrderDTO addOrderDTO)
        {
            var order = _mapper.Map<Order>(addOrderDTO);
            order.UserId = userId;
            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();
            var orderDtoResult = _mapper.Map<OrderDTO>(order);
            return new Response<OrderDTO>(orderDtoResult);
        }

        public async Task<Response> DeleteOrderAsync(string userId, int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                return new Response("Order not found");

            if (order.UserId != userId)
                return new Response("You do not have permission to delete this order");

            await _orderRepository.RemoveAsync(order);
            await _orderRepository.SaveChangesAsync();
            return new Response() { IsSuccessful = true };
        }

        public async Task<Response<List<OrderDTO>>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            var orderDtos = _mapper.Map<List<OrderDTO>>(orders);
            return new Response<List<OrderDTO>>(orderDtos);
        }

        public async Task<Response<OrderDTO>> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                return new Response<OrderDTO>("Order not found");

            var orderDto = _mapper.Map<OrderDTO>(order);
            return new Response<OrderDTO>(orderDto);
        }

        public async Task<Response<OrderDTO>> UpdateOrderAsync(string userId, int orderId, UpdateOrderDTO updateOrderDTO)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                return new Response<OrderDTO>("Order not found");

            if (order.UserId != userId)
                return new Response<OrderDTO>("You do not have permission to update this order!");

            var updatedOrder = _mapper.Map(updateOrderDTO, order);
            updatedOrder.UserId = userId;
            updatedOrder.Id = orderId;
            await _orderRepository.SaveChangesAsync();
            var orderDtoResult = _mapper.Map<OrderDTO>(order);
            return new Response<OrderDTO>(orderDtoResult);
        }
    }
}