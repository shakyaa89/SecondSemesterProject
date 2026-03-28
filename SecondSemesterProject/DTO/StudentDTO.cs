using System.ComponentModel.DataAnnotations;

namespace SecondSemesterProject.DTO
{
    public class StudentDTO
    {
        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [Required]
        public required string DateOfBirth { get; set; }

        [Required]
        public required string Phone { get; set; }

        [Required]
        public required string Email { get; set; }

    }
}
