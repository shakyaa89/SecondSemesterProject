using System.ComponentModel.DataAnnotations;

namespace SecondSemesterProject.Domain.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int DurationYears { get; set; }
    }
}
