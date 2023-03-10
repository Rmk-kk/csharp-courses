namespace NetCoreCourse.Models;

public class Address : BaseModel
{
    public string Street {get; set;} = null!;
    public string City {get; set;} = null!;
    public string ZipCode {get; set;} = null!;
}