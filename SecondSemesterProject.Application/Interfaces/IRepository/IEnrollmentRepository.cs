using SecondSemesterProject.Domain.Models;

namespace SecondSemesterProject.Application.Interfaces.IRepository
{
    public interface IEnrollmentRepository
    {
        Task<List<Enrollment>> GetAllEnrollments();
        Task EnrollStudent(Enrollment enrollment);
        Task RemoveEnrollment(int id);
        Task BulkEnroll(List<Enrollment> enrollments);
        Task<object> GetEnrollmentFullDetails();
        Task<int> GetEnrollmentCount();
        Task<List<Enrollment>> GetEnrollmentsByDate(DateTime date);
    }
}
