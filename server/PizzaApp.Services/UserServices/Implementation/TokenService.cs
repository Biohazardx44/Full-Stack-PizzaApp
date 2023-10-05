using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PizzaApp.Domain.Entities;
using PizzaApp.Services.UserServices.Abstraction;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PizzaApp.Services.UserServices.Implementation
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<JwtSecurityToken> GenerateTokenAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id)
            };

            var token = GetJWT(claims);
            return Task.FromResult(token);
        }

        private JwtSecurityToken GetJWT(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["PizzaAppSettings:Token"]));

            var token = new JwtSecurityToken
                (
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.UtcNow.AddYears(1),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}