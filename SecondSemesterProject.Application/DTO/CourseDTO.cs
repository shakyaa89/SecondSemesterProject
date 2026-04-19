using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using SecondSemesterProject.Application.DTO;

namespace SecondSemesterProject.Application.DTO
{
    public class CourseDTO
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        public required int DurationYears { get; set; }

        public List<ModuleDTO>? Modules { get; set; }
    }
}
