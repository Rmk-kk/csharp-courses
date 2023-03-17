using Microsoft.AspNetCore.Mvc;
using NetCoreCourse.DTOs;
using NetCoreCourse.Services;

namespace NetCoreCourse.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("/signup")]
        public async Task<IActionResult> SignUp(UserSignUpDTO request)
        {
            var user = await _service.SignUpAsync(request);
            if(user is null)
            {
                return BadRequest();
            }
            return Ok(user);
        }
    }
}