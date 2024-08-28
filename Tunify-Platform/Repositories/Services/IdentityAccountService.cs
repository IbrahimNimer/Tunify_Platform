using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Identity.Client;
using System.Security.Claims;
using Tunify_Platform.Models;
using Tunify_Platform.Models.DTOs;
using Tunify_Platform.Repositories.Interfaces;

namespace Tunify_Platform.Repositories.Services
{
    public class IdentityAccountService : IAccounts

    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtTokenService _JwtTokenService;

        public IdentityAccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, JwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _JwtTokenService = jwtTokenService;
        }

        public async Task<UserDto> Register(RegisterDto registerEmployeeDTO, ModelStateDictionary modelState)
        {
            var employee = new ApplicationUser
            {
                UserName = registerEmployeeDTO.UserName,
                Email = registerEmployeeDTO.Email,
                FirstName = registerEmployeeDTO.FirstName, 
                LastName = registerEmployeeDTO.LastName

            };

            var result = await _userManager.CreateAsync(employee, registerEmployeeDTO.Password);
            await _userManager.AddToRolesAsync(employee, registerEmployeeDTO.Roles);
            if (result.Succeeded)
            {
                return new UserDto
                {
                    Id = employee.Id,
                    Username = employee.UserName,

                };
            }

            foreach (var error in result.Errors)
            {
                var errorCode = error.Code.Contains("Password") ? nameof(registerEmployeeDTO.Password) :
                                error.Code.Contains("Email") ? nameof(registerEmployeeDTO.Email) :
                                error.Code.Contains("UserName") ? nameof(registerEmployeeDTO.UserName) : "UnknownError";

                modelState.AddModelError(errorCode, error.Description);
            }
            return null;
        }

        public async Task<UserDto> LoginUser(string Username, string Password)
        {
            var user = await _userManager.FindByNameAsync(Username);
            if (user == null) 
            {
                return null;
            }

            bool passValedation = await _userManager.CheckPasswordAsync(user, Password);

            if (passValedation)
            {
                return new UserDto()
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Token = await _JwtTokenService.GenerateToken(user, TimeSpan.FromMinutes(3)) 
                };
            }
            return null;
        }


        public async Task<UserDto> LogoutUser(string Username)
        {
            if (string.IsNullOrEmpty(Username)) 
            {
                throw new ArgumentException("Username cannot be null or empty.");
            }

            var user = await _userManager.FindByNameAsync(Username);
            if (user == null) 
            {
                return null;
            }

            await _signInManager.SignOutAsync();
            return new UserDto()
            {
                Id = user.Id,
                Username = user.UserName
            };
        }



        public async Task<UserDto> GetToken(ClaimsPrincipal claimsPrincipal)
        {
            var newToken = await _userManager.GetUserAsync(claimsPrincipal);
            if (newToken == null)
            {
                throw new InvalidOperationException("Token Is Not Exist!");
            }
            return new UserDto()
            {
                Id = newToken.Id,
                Username = newToken.UserName,
                Token = await _JwtTokenService.GenerateToken(newToken, System.TimeSpan.FromMinutes(5)) 
            };
        }


        public async Task<UserProfileDto> GetProfile(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return null;

            return new UserProfileDto
            {
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
                
            };
        }

        public async Task<UserProfileDto> UpdateProfile(string username, UpdateProfileDto updateProfileDto)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return null;

            user.FirstName = updateProfileDto.FirstName;
            user.LastName = updateProfileDto.LastName;
            user.Email = updateProfileDto.Email;
            user.PhoneNumber = updateProfileDto.PhoneNumber;
            

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return null;

            return new UserProfileDto
            {
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
                
            };
        }

    }
}
