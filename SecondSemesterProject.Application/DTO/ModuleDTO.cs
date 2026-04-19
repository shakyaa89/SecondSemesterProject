using System.ComponentModel.DataAnnotations;

namespace SecondSemesterProject.Application.DTO
{
    public class ModuleDTO
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public int Credits { get; set; }
        
        public int CourseId { get; set; }
    }
}
