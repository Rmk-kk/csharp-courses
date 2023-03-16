using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreCourse.Models
{
    public class Project : BaseModel
    {
        public string Name {get; set;} = null!;
        
        [Column(TypeName = "jsonb")]
        public ICollection<string> Tags {get; set;} = null!;
        public DateTime Deadline {get; set;}
        public string? Description {get; set;}
        
        public ICollection<ProjectStudent> StudentLinks {get; set;} = null!;
    }
}