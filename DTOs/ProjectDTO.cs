using NetCoreCourse.Models;

namespace NetCoreCourse.DTOs
{
    public class ProjectDTO : BaseDTO<Project>
    {
        public string Name {get; set;} = null!;
        public ICollection<string> Tags {get; set;} = null!;
        public DateTime Deadline {get; set;}
        public string? Description {get; set;}
 
        public override void UpdateModel(Project model)
        {
            model.Name = Name;
            model.Tags = Tags;
            model.Deadline = Deadline;
            model.Description = Description;
        }
    }
}