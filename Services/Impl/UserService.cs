
using NetCoreCourse.DTOs;
using Microsoft.AspNetCore.Identity;
using NetCoreCourse.Models;

namespace NetCoreCourse.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly ITokenService _tokenService;
        public UserService(UserManager<User> userManager, ITokenService tokenService, RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _roleManager = roleManager;
        }

        public async Task<UserSignInResponseDTO?> SignInAsync(UserSignInDTO request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);  
            if(user is null)
            {
                return null;
            }          
            if(!await _userManager.CheckPasswordAsync(user, request.Password)) 
            {
                return null;
            }
            return await _tokenService.GenerateTokenAsync(user);
        }

        public async Task<User?> SignUpAsync(UserSignUpDTO request)
        {
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Email,
                Email = request.Email
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if(!result.Succeeded)
            {
                return null;
            }

            var roles = new[] { "Admin", "Dev" };
            foreach (var role in roles)
            {
                if (await _roleManager.FindByNameAsync(role) is null)
                {
                    await _roleManager.CreateAsync(new IdentityRole<int>
                    {
                        Name = role,
                    });
                }
            }
            await _userManager.AddToRolesAsync(user, roles);
            
            return user;
        }
    }
}