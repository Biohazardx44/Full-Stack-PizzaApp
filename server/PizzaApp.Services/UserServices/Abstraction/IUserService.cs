using PizzaApp.DTOs.UserDTOs;
using PizzaApp.Shared.Requests;
using PizzaApp.Shared.Responses;
using System.Security.Claims;

namespace PizzaApp.Services.UserServices.Abstraction
{
    public interface IUserService
    {
        Task<Response<RegisterUserResponse>> RegisterUserAsync(RegisterUserRequest registerUserRequest);
        Task<Response<LoginUserResponse>> LoginUserAsync(LoginUserRequest loginUserRequest);
        Task<Response> GetAllUsersAsync();
        Task<Response<UserDTO>> GetUserByIdAsync(string id);
        Task<Response<UserDTO>> UpdateUserAsync(string id, UserDTO updatedUser);
        Task<Response> DeleteUserAsync(string id, ClaimsPrincipal userClaims);
    }
}