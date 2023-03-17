using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCoreCourse.DTOs;
using NetCoreCourse.Models;

namespace NetCoreCourse.Services
{
    public interface ITokenService
    {
        Task<UserSignInResponseDTO> GenerateTokenAsync(User user);
    }
}