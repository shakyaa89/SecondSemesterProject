namespace SecondSemesterProject.Application.DTO
{
    public class EnrollmentDTO
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public string EnrolledDate { get; set; } = string.Empty;
    }
}
