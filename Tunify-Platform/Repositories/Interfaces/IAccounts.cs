using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;
using Tunify_Platform.Models.DTOs;

namespace Tunify_Platform.Repositories.Interfaces
{
    public interface IAccounts
    {

        //Add regester
        Task<UserDto> Register(RegisterDto registerEmployeeDTO, ModelStateDictionary modelState);

        // Add Login 
        public Task<UserDto> LoginUser(string Username, string Password);

        public Task<UserDto> LogoutUser(string Username);

        public Task<UserDto> GetToken(ClaimsPrincipal claimsPrincipal);

        Task<UserProfileDto> GetProfile(string username);
        Task<UserProfileDto> UpdateProfile(string username, UpdateProfileDto updateProfileDto);

    }
}
