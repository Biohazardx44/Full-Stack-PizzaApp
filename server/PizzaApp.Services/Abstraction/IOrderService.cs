using PizzaApp.DTOs.OrderDTOs;
using PizzaApp.Shared.Responses;

namespace PizzaApp.Services.Abstraction
{
    public interface IOrderService
    {
        Task<Response<List<OrderDTO>>> GetAllOrdersAsync();
        Task<Response<OrderDTO>> GetOrderByIdAsync(int id);
        Task<Response<OrderDTO>> CreateOrderAsync(string userId, AddOrderDTO addOrderDTO);
        Task<Response<OrderDTO>> UpdateOrderAsync(string userId, int orderId, UpdateOrderDTO updateOrderDTO);
        Task<Response> DeleteOrderAsync(string userId, int orderId);
    }
}