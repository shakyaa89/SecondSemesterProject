using Microsoft.EntityFrameworkCore;
using SecondSemesterProject.Infrastructure.Persistence;
using SecondSemesterProject.Application.Interfaces.IRepository;


namespace SecondSemesterProject.Infrastructure.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public DashboardRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> GetSummary()
        {
            var students = await _dbContext.Student.CountAsync();
            var courses = await _dbContext.Course.CountAsync();
            var modules = await _dbContext.Module.CountAsync();
            var instructors = await _dbContext.Instructor.CountAsync();
            var enrollments = await _dbContext.Enrollment.CountAsync();
            var assignments = await _dbContext.ModuleInstructor.CountAsync();

            return new
            {
                Students = students,
                Courses = courses,
                Modules = modules,
                Instructors = instructors,
                Enrollments = enrollments,
                ModuleInstructorAssignments = assignments
            };
        }
    }
}
