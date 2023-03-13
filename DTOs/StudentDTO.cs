namespace NetCoreCourse.DTOs;

using System.ComponentModel.DataAnnotations;
using NetCoreCourse.Models;

public class StudentDTO : BaseDTO<Student>
{
    [MinLength(3)]
    public string FirstName {get; set;} = null!;

    [MinLength(3)]
    public string LastName {get; set;} = null!;

    [EmailAddress]
    public string Email {get; set;} = null!;

    public AddressDTO Address {get; set;} = null!;

    public int CourseId {get; set;}

    public override void UpdateModel(Student model)
    {
       model.FirstName = FirstName;
       model.LastName = LastName;
       model.Email = Email;
       model.CourseId = CourseId;

       var address = new Address();
       Address.UpdateModel(address);     
       model.Address = address;

    //    var course = new Course();
    //    Course.UpdateModel(course);
    //    model.Course = course;
    }
}