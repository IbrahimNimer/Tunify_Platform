using Microsoft.AspNetCore.Mvc.ModelBinding;
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

    }
}
