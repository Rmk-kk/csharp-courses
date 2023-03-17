using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using NetCoreCourse.Models;

namespace NetCoreCourse.DTOs
{
    public class UserSignUpResponseDTO
    {
        public int Id {get; set;}
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        [EmailAddress]
        public string Email { get; set; } = null!;

        public static UserSignUpResponseDTO UpdateFromUser(User user)
        {
            return new UserSignUpResponseDTO 
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email!
            };
        }  
    }
}