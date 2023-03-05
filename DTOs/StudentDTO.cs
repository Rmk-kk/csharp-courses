namespace NetCoreCourse.DTOs;

using System.ComponentModel.DataAnnotations;

public class StudentDTO
{
    [MinLength(3)]
    public string FirstName {get; set;}

    [MinLength(3)]
    public string LastName {get; set;} 

    [EmailAddress]
    public string Email {get; set;}
}