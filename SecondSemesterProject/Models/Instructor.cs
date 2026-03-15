using System.ComponentModel.DataAnnotations;

namespace SecondSemesterProject.Models
{
    public class Instructor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string HireDate { get; set; } = string.Empty;
    }
}
