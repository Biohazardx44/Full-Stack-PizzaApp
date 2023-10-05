using PizzaApp.DTOs.PizzaDTOs;
using PizzaApp.Shared.Responses;

namespace PizzaApp.Services.Abstraction
{
    public interface IPizzaService
    {
        Task<Response<List<PizzaDTO>>> GetAllPizzasAsync();
        Task<Response<PizzaDTO>> GetPizzaByIdAsync(int id);
        Task<Response<PizzaDTO>> CreatePizzaAsync(string userId, AddPizzaDTO addPizzaDTO);
        Task<Response<PizzaDTO>> UpdatePizzaAsync(string userId, int pizzaId, UpdatePizzaDTO updatePizzaDTO);
        Task<Response> DeletePizzaAsync(string userId, int pizzaId);
    }
}