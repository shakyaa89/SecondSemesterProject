using SecondSemesterProject.Application.DTO;
using SecondSemesterProject.Domain.Models;

namespace SecondSemesterProject.Application.Interfaces.IService
{
    public interface IEnrollmentService
    {
        Task<List<Enrollment>> GetAllEnrollments();
        Task EnrollStudent(EnrollmentDTO enrollment);
        Task RemoveEnrollment(int id);
        Task BulkEnroll(List<EnrollmentDTO> enrollments);
        Task<object> GetEnrollmentFullDetails();
        Task<int> GetEnrollmentCount();
        Task<List<Enrollment>> GetEnrollmentsByDate(DateTime date);
    }
}
