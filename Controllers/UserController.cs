using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreCourse.DTOs;
using NetCoreCourse.Services;

namespace NetCoreCourse.Controllers
{
    [Authorize]
    public class UserController : ApiControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }
        
        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(UserSignUpDTO request)
        {
            var user = await _service.SignUpAsync(request);
            if(user is null)
            {
                return BadRequest();
            }
            return Ok(UserSignUpResponseDTO.UpdateFromUser(user));
        }
        
        [AllowAnonymous]
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(UserSignInDTO request)
        {
            var response = await _service.SignInAsync(request);
            if(response is null)
            {
                return Unauthorized();
                // return Forbid();
            }
            else 
            {
                return Ok(response);
            }
        }
    }
}