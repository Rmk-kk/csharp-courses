namespace NetCoreCourse.DTOs;

using NetCoreCourse.Models;

public class AssigmentDTO : BaseDTO<Assigment>
{
    public DateTime Deadline {get; set;}
    public string Title {get; set;} = string.Empty;
    public string Description {get; set;} = string.Empty;
    
    public override void UpdateModel(Assigment model)
    {
        model.Title = Title;
        model.Description = Description;
        model.Deadline = Deadline;
    }
}