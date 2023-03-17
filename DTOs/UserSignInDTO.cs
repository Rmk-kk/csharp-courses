using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreCourse.DTOs
{
    public class UserSignInDTO
    {
        public string Email {get; set;} = null!;
        public string Password {get; set;} = null!;
    }
}