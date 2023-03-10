namespace NetCoreCourse.DTOs;

using NetCoreCourse.Models;

public class AddressDTO : BaseDTO<Address>
{
    public string Street {get; set;} = null!;
    public string City {get; set;} = null!;
    public string ZipCode {get; set;} = null!;

    public override void UpdateModel(Address model)
    {
        model.Street = Street;
        model.City = City;
        model.ZipCode = ZipCode;
    }
}