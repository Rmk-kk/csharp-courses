using NetCoreCourse.Models;
using NetCoreCourse.DTOs;

namespace NetCoreCourse.Services
{
    public interface IUserService
    {
        Task<User> SignUpAsync(UserSignUpDTO request);
    }
}