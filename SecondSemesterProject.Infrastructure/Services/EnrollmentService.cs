using SecondSemesterProject.Application.DTO;
using SecondSemesterProject.Application.Interfaces.IRepository;
using SecondSemesterProject.Application.Interfaces.IService;
using SecondSemesterProject.Domain.Models;

namespace SecondSemesterProject.Infrastructure.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;

        public EnrollmentService(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        public async Task<List<Enrollment>> GetAllEnrollments() => await _enrollmentRepository.GetAllEnrollments();

        public async Task EnrollStudent(EnrollmentDTO enrollment)
        {
            var model = new Enrollment
            {
                StudentId = enrollment.StudentId,
                CourseId = enrollment.CourseId,
                EnrolledDate = enrollment.EnrolledDate
            };

            await _enrollmentRepository.EnrollStudent(model);
        }

        public async Task RemoveEnrollment(int id) => await _enrollmentRepository.RemoveEnrollment(id);

        public async Task BulkEnroll(List<EnrollmentDTO> enrollments)
        {
            var models = enrollments.Select(e => new Enrollment
            {
                StudentId = e.StudentId,
                CourseId = e.CourseId,
                EnrolledDate = e.EnrolledDate
            }).ToList();

            await _enrollmentRepository.BulkEnroll(models);
        }

        public async Task<object> GetEnrollmentFullDetails() => await _enrollmentRepository.GetEnrollmentFullDetails();

        public async Task<int> GetEnrollmentCount() => await _enrollmentRepository.GetEnrollmentCount();

        public async Task<List<Enrollment>> GetEnrollmentsByDate(DateTime date) => await _enrollmentRepository.GetEnrollmentsByDate(date);
    }
}
