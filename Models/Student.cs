namespace NetCoreCourse.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Student : BaseModel
{
    public string FirstName {get; set;} = null!;
    public string LastName {get; set;} = null!;

    //ignore creation in DB
    [NotMapped]
    [JsonIgnore]
    public string FullName 
    {
        get => $"{FirstName} {LastName}";
    }

    [MaxLength(256)]
    public string Email {get; set;} = null!;

    //Mark as virtual to use lazy loading proxies
    public Address? Address {get; set;}

    //don't return this with JSON response
    [JsonIgnore]
    public int? AddressId {get; set;} 
    
    public Course? Course {get; set;}
    public int CourseId {get; set;}

    public ICollection<Assigment> Assigments {get; set;} = null!;
}