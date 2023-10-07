using AutoMapper;
using PizzaApp.DataAccess.Repositories.Abstraction;
using PizzaApp.Domain.Entities;
using PizzaApp.DTOs.PizzaDTOs;
using PizzaApp.Services.Abstraction;
using PizzaApp.Shared.Responses;

namespace PizzaApp.Services.Implementation
{
    public class PizzaService : IPizzaService
    {
        private readonly IPizzaRepository _pizzaRepository;
        private readonly IMapper _mapper;

        public PizzaService(IPizzaRepository pizzaRepository, IMapper mapper)
        {
            _pizzaRepository = pizzaRepository;
            _mapper = mapper;
        }

        public async Task<Response<PizzaDTO>> CreatePizzaAsync(string userId, AddPizzaDTO addPizzaDTO)
        {
            var pizza = _mapper.Map<Pizza>(addPizzaDTO);
            pizza.UserId = userId;
            await _pizzaRepository.AddAsync(pizza);
            await _pizzaRepository.SaveChangesAsync();
            var pizzaDtoResult = _mapper.Map<PizzaDTO>(pizza);
            return new Response<PizzaDTO>(pizzaDtoResult);
        }

        public async Task<Response> DeletePizzaAsync(string userId, int pizzaId)
        {
            var pizza = await _pizzaRepository.GetByIdAsync(pizzaId);
            if (pizza == null)
                return new Response("Pizza not found");

            if (pizza.UserId != userId)
                return new Response("You do not have permission to delete this pizza");

            await _pizzaRepository.RemoveAsync(pizza);
            await _pizzaRepository.SaveChangesAsync();
            return new Response() { IsSuccessful = true };
        }

        public async Task<Response<List<PizzaDTO>>> GetAllPizzasAsync()
        {
            var pizzas = await _pizzaRepository.GetAllAsync();
            var pizzaDtos = _mapper.Map<List<PizzaDTO>>(pizzas);
            return new Response<List<PizzaDTO>>(pizzaDtos);
        }

        public async Task<Response<PizzaDTO>> GetPizzaByIdAsync(int id)
        {
            var pizza = await _pizzaRepository.GetByIdAsync(id);
            if (pizza == null)
                return new Response<PizzaDTO>("Pizza not found");

            var pizzaDto = _mapper.Map<PizzaDTO>(pizza);
            return new Response<PizzaDTO>(pizzaDto);
        }

        public async Task<Response<PizzaDTO>> UpdatePizzaAsync(string userId, int pizzaId, UpdatePizzaDTO updatePizzaDto)
        {
            var pizza = await _pizzaRepository.GetByIdAsync(pizzaId);
            if (pizza == null)
                return new Response<PizzaDTO>("Pizza not found!");

            if (pizza.UserId != userId)
                return new Response<PizzaDTO>("You do not have permission to update this pizza!");

            var updatedPizza = _mapper.Map(updatePizzaDto, pizza);
            updatedPizza.UserId = userId;
            updatedPizza.Id = pizzaId;
            await _pizzaRepository.SaveChangesAsync();
            var pizzaDtoResult = _mapper.Map<PizzaDTO>(pizza);
            return new Response<PizzaDTO>(pizzaDtoResult);
        }
    }
}