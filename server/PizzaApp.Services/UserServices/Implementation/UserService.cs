using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PizzaApp.Domain.Entities;
using PizzaApp.DTOs.UserDTOs;
using PizzaApp.Services.UserServices.Abstraction;
using PizzaApp.Shared.CustomExceptions.UserExceptions;
using PizzaApp.Shared.Requests;
using PizzaApp.Shared.Responses;
using System.IdentityModel.Tokens.Jwt;

namespace PizzaApp.Services.UserServices.Implementation
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<Response> DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return new Response("User not found");

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                return new Response(result.Errors.Select(x => x.Description));

            return new Response { IsSuccessful = true };
        }

        public async Task<Response> GetAllUsersAsync()
        {
            var response = new Response<List<UserDTO>>();
            var users = await _userManager.Users.ToListAsync();
            var userDtos = _mapper.Map<List<UserDTO>>(users);

            response.Result = userDtos;
            response.IsSuccessful = true;
            return response;
        }

        public async Task<Response<UserDTO>> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return new Response<UserDTO>("User not found");

            var userDto = _mapper.Map<UserDTO>(user);

            return new Response<UserDTO>(userDto);
        }

        public async Task<Response<LoginUserResponse>> LoginUserAsync(LoginUserRequest loginUserRequest)
        {
            ValidateLoginUserRequest(loginUserRequest);

            var user = await _userManager.FindByNameAsync(loginUserRequest.Username);
            if (user == null)
                return new("User does not exist");

            var passwordIsValid = await _userManager.CheckPasswordAsync(user, loginUserRequest.Password);
            if (!passwordIsValid)
                return new("Invalid password");

            var token = await _tokenService.GenerateTokenAsync(user);

            return new Response<LoginUserResponse>(new LoginUserResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ValidTo = token.ValidTo
            });
        }

        public async Task<Response<RegisterUserResponse>> RegisterUserAsync(RegisterUserRequest registerUserRequest)
        {
            ValidateRegisterUserRequest(registerUserRequest);

            var existingUser = await _userManager.FindByNameAsync(registerUserRequest.Username);
            if (existingUser != null)
                return new Response<RegisterUserResponse>($"The username {registerUserRequest.Username} is already in use!");

            var user = new UserDTO { UserName = registerUserRequest.Username, Email = registerUserRequest.Email };
            var result = await _userManager.CreateAsync(user, registerUserRequest.Password);

            if (!result.Succeeded)
                return new(result.Errors.Select(x => x.Description));

            return new(new RegisterUserResponse
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email
            });
        }

        public async Task<Response<UserDTO>> UpdateUserAsync(string id, UserDTO updatedUser)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return new Response<UserDTO>("User not found");

            ValidateUserInput(updatedUser);

            var existingUserWithUpdatedUserName = await _userManager.FindByNameAsync(updatedUser.UserName);
            if (existingUserWithUpdatedUserName != null && existingUserWithUpdatedUserName.Id != id)
                return new Response<UserDTO>($"The username '{updatedUser.UserName}' is already in use by another user!");

            var existingUserWithUpdatedEmail = await _userManager.FindByEmailAsync(updatedUser.Email);
            if (existingUserWithUpdatedEmail != null && existingUserWithUpdatedEmail.Id != id)
                return new Response<UserDTO>($"The email '{updatedUser.Email}' is already in use by another user!");

            user.UserName = updatedUser.UserName;
            user.Email = updatedUser.Email;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return new Response<UserDTO>(result.Errors.Select(x => x.Description));

            return new Response<UserDTO>(updatedUser);
        }

        private void ValidateRegisterUserRequest(RegisterUserRequest registerUserRequest)
        {
            if (string.IsNullOrWhiteSpace(registerUserRequest.Username))
                throw new UserDataException("Username is a required field!");

            if (string.IsNullOrWhiteSpace(registerUserRequest.Email))
                throw new UserDataException("Email is a required field!");

            if (string.IsNullOrWhiteSpace(registerUserRequest.Password))
                throw new UserDataException("Password is a required field!");

            if (registerUserRequest.Username.Length < 5 || registerUserRequest.Username.Length > 30)
                throw new UserDataException("Username must be between 5 and 30 characters long!");

            if (registerUserRequest.Password.Length < 5 || registerUserRequest.Password.Length > 30)
                throw new UserDataException("Password must be between 5 and 30 characters long!");

            if (registerUserRequest.Email.Length > 100)
                throw new UserDataException("Email cannot exceed 100 characters!");

            if (!registerUserRequest.Email.Contains("@") || registerUserRequest.Email.IndexOf("@") == registerUserRequest.Email.Length - 1)
                throw new UserDataException("Invalid email format. Email must contain '@' and at least one character after it!");
        }

        private void ValidateLoginUserRequest(LoginUserRequest loginUserRequest)
        {
            if (string.IsNullOrWhiteSpace(loginUserRequest?.Username))
                throw new UserDataException("Username is a required field!");

            if (string.IsNullOrWhiteSpace(loginUserRequest?.Password))
                throw new UserDataException("Password is a required field!");
        }

        private void ValidateUserInput(UserDTO updatedUser)
        {
            if (string.IsNullOrWhiteSpace(updatedUser.UserName))
                throw new UserDataException("Username is a required field!");

            if (string.IsNullOrWhiteSpace(updatedUser.Email))
                throw new UserDataException("Email is a required field!");

            if (updatedUser.UserName.Length < 5 || updatedUser.UserName.Length > 30)
                throw new UserDataException("Username must be between 5 and 30 characters long!");

            if (updatedUser.Email.Length > 100)
                throw new UserDataException("Email cannot exceed 100 characters!");

            if (!updatedUser.Email.Contains("@") || updatedUser.Email.IndexOf("@") == updatedUser.Email.Length - 1)
                throw new UserDataException("Invalid email format. Email must contain '@' and at least one character after it!");
        }
    }
}