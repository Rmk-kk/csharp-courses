using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreCourse.DTOs
{
    public class UserSignInResponseDTO
    {
        public string Token {get; set;} = null!;
        public DateTime Expiration {get; set;}
    }
}