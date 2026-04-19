using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecondSemesterProject.Domain.Models
{
    public class ModuleInstructor
    {


        [Key]
        public int Id { get; set; }

        [Required]
        public int ModuleId { get; set; }

        [Required]
        public int InstructorId { get; set; }

        [ForeignKey(nameof(ModuleId))]
        public virtual Module? Module {  get; set; }

        [ForeignKey(nameof(InstructorId))]
        public virtual Instructor? Instructor { get; set; }
    }
}
