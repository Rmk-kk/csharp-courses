using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreCourse.DTOs
{
    public class UserSignUpDTO
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        [EmailAddress]
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}