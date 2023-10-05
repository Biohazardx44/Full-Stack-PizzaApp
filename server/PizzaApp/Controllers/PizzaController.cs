using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzaApp.Domain.Entities;
using PizzaApp.DTOs.PizzaDTOs;
using PizzaApp.Services.Abstraction;
using PizzaApp.Shared.CustomExceptions.PizzaExceptions;
using PizzaApp.Shared.CustomExceptions.ServerExceptions;
using System.Security.Claims;

namespace PizzaApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : BaseController
    {
        private readonly IPizzaService _pizzaService;
        private readonly UserManager<User> _userManager;

        public PizzaController(IPizzaService pizzaService, UserManager<User> userManager)
        {
            _pizzaService = pizzaService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPizzas()
        {
            try
            {
                var response = await _pizzaService.GetAllPizzasAsync();
                return Response(response);
            }
            catch (PizzaDataException e)
            {
                return BadRequest(e.Message);
            }
            catch (InternalServerErrorException e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPizzaById(int id)
        {
            try
            {
                var response = await _pizzaService.GetPizzaByIdAsync(id);
                return Response(response);
            }
            catch (PizzaNotFoundException e)
            {
                return BadRequest(e.Message);
            }
            catch (InternalServerErrorException e)
            {
                return StatusCode(statusCode: 500, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePizza([FromBody] AddPizzaDTO addPizzaDTO)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var response = await _pizzaService.CreatePizzaAsync(userId, addPizzaDTO);
                return Response(response);
            }
            catch (PizzaDataException e)
            {
                return BadRequest(e.Message);
            }
            catch (InternalServerErrorException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePizza(int id, [FromBody] UpdatePizzaDTO updatePizzaDto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var response = await _pizzaService.UpdatePizzaAsync(userId, id, updatePizzaDto);
                return Response(response);
            }
            catch (PizzaDataException e)
            {
                return BadRequest(e.Message);
            }
            catch (InternalServerErrorException e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePizza(int id)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var response = await _pizzaService.DeletePizzaAsync(userId, id);
                return Response(response);
            }
            catch (PizzaDataException e)
            {
                return BadRequest(e.Message);
            }
            catch (InternalServerErrorException e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}