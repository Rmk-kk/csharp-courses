namespace NetCoreCourse.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Student : BaseModel
{
    public string FirstName {get; set;} = null!;
    public string LastName {get; set;} = null!;

    //ignore creation in DB
    [NotMapped]
    public string FullName 
    {
        get => $"{FirstName} {LastName}";
    }

    [MaxLength(256)]
    public string Email {get; set;} = null!;

    public Address? Address {get; set;}
    public int? AddressId {get; set;} 
}