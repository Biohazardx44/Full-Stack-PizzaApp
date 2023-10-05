using PizzaApp.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace PizzaApp.Services.UserServices.Abstraction
{
    public interface ITokenService
    {
        Task<JwtSecurityToken> GenerateTokenAsync(User user);
    }
}