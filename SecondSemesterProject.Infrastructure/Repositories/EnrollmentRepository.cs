using Microsoft.EntityFrameworkCore;
using SecondSemesterProject.Domain.Models;
using SecondSemesterProject.Infrastructure.Persistence;
using SecondSemesterProject.Application.Interfaces.IRepository;


namespace SecondSemesterProject.Infrastructure.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public EnrollmentRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Enrollment>> GetAllEnrollments() => await _dbContext.Enrollment.ToListAsync();

        public async Task EnrollStudent(Enrollment enrollment)
        {
            await _dbContext.Enrollment.AddAsync(enrollment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveEnrollment(int id)
        {
            var existing = await _dbContext.Enrollment.FindAsync(id);
            if (existing == null) return;

            _dbContext.Enrollment.Remove(existing);
            await _dbContext.SaveChangesAsync();
        }

        public async Task BulkEnroll(List<Enrollment> enrollments)
        {
            await _dbContext.Enrollment.AddRangeAsync(enrollments);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<object> GetEnrollmentFullDetails()
        {
            var enrollments = await _dbContext.Enrollment
                .Include(e => e.Student)
                .Include(e => e.Course)
                .ToListAsync();

            return enrollments.Select(e => (object)new
            {
                e.Id,
                e.StudentId,
                Student = e.Student == null ? null : new { e.Student.Id, e.Student.FirstName, e.Student.LastName, e.Student.Email },
                e.CourseId,
                Course = e.Course == null ? null : new { e.Course.Id, e.Course.Name, e.Course.DurationYears },
                e.EnrolledDate
            }).ToList();
        }

        public async Task<int> GetEnrollmentCount() => await _dbContext.Enrollment.CountAsync();

        public async Task<List<Enrollment>> GetEnrollmentsByDate(DateTime date)
        {
            var enrollments = await _dbContext.Enrollment.ToListAsync();

            return enrollments
                .Where(e => DateTime.TryParse(e.EnrolledDate, out var parsed) && parsed.Date == date.Date)
                .ToList();
        }
    }
}
