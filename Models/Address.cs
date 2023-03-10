namespace NetCoreCourse.Models;

public class Address : BaseModel
{
    public string Street {get; set;}
    public string City {get; set;}
    public int ZipCode {get; set;}
}