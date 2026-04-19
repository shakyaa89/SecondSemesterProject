using Microsoft.EntityFrameworkCore;
using SecondSemesterProject.Domain.Models;
using SecondSemesterProject.Infrastructure.Persistence;
using SecondSemesterProject.Application.Interfaces.IRepository;
using System.Linq;

namespace SecondSemesterProject.Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public CourseRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<List<object>> FetchAllCoursesWithModuleCountAsync()
        {
            var modules = await _dbContext.Module.ToListAsync();

            var courses = await _dbContext.Course.ToListAsync();

            return courses.Select(c => (object)new
            {
                c.Id,
                c.Name,
                c.DurationYears,
                ModuleCount = modules.Count(m => m.CourseId == c.Id)
            }).ToList();
        }

        public async Task<Course?> FetchCourseByIdAsync(int id) => await _dbContext.Course.FindAsync(id);

        public async Task<object?> FetchCourseWithModulesAsync(int id)
        {
            var course = await _dbContext.Course.FirstOrDefaultAsync(c => c.Id == id);

            if (course == null) return null;

            var modules = await _dbContext.Module.Where(m => m.CourseId == id).ToListAsync();

            return new {
                course.Id,
                course.Name,
                course.DurationYears,
                Modules = modules.Select(m => new { m.Id, m.Title, m.Credits }).ToList()
            };
        }

        public async Task<List<Module>> FetchCourseModulesAsync(int id) => await _dbContext.Module.Where(m => m.CourseId == id).ToListAsync();

        public async Task<List<Student>> FetchCourseStudentsAsync(int id)
        {
            var enrollments = await _dbContext.Enrollment.Where(e => e.CourseId == id).Include(e => e.Student).ToListAsync();

            return enrollments.Where(e => e.Student != null).Select(e => e.Student!).GroupBy(s => s!.Id).Select(g => g.First()).ToList()!;
        }

        public async Task AddCourseAsync(Course course, List<Module>? modules = null)
        {
            await _dbContext.Course.AddAsync(course);
            await _dbContext.SaveChangesAsync();

            if (modules != null && modules.Any())
            {
                foreach (var m in modules)
                {
                    m.CourseId = course.Id;
                }

                await _dbContext.Module.AddRangeAsync(modules);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task AddModuleToCourseAsync(int courseId, Module module)
        {
            var course = await _dbContext.Course.FindAsync(courseId);

            if (course == null) return;

            module.CourseId = courseId;
            await _dbContext.Module.AddAsync(module);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCourseAsync(int id, Course course, List<Module>? modules = null)
        {
            var existing = await _dbContext.Course.FindAsync(id);
            if (existing == null) return;

            existing.Name = course.Name;
            existing.DurationYears = course.DurationYears;

            // Remove existing modules
            var existingModules = await _dbContext.Module.Where(m => m.CourseId == id).ToListAsync();
            if (existingModules.Any())
            {
                _dbContext.Module.RemoveRange(existingModules);
            }

            if (modules != null && modules.Any())
            {
                foreach (var m in modules)
                {
                    m.CourseId = id;
                }

                await _dbContext.Module.AddRangeAsync(modules);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            var existing = await _dbContext.Course.FindAsync(id);
            if (existing == null) return false;

            var enrollments = await _dbContext.Enrollment.Where(e => e.CourseId == id).ToListAsync();
            if (enrollments.Any()) _dbContext.Enrollment.RemoveRange(enrollments);

            var modules = await _dbContext.Module.Where(m => m.CourseId == id).ToListAsync();
            if (modules.Any())
            {
                var moduleIds = modules.Select(m => m.Id).ToList();
                var moduleInstructors = await _dbContext.ModuleInstructor.Where(mi => moduleIds.Contains(mi.ModuleId)).ToListAsync();
                if (moduleInstructors.Any()) _dbContext.ModuleInstructor.RemoveRange(moduleInstructors);

                _dbContext.Module.RemoveRange(modules);
            }

            _dbContext.Course.Remove(existing);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task AddCoursesAsync(List<Course> courses)
        {
            await _dbContext.Course.AddRangeAsync(courses);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<object>> FetchCoursesWithDetailsAsync()
        {
            var courses = await _dbContext.Course.ToListAsync();
            var modules = await _dbContext.Module.ToListAsync();
            var moduleInstructors = await _dbContext.ModuleInstructor.ToListAsync();
            var instructors = await _dbContext.Instructor.ToListAsync();

            return courses.Select(course => (object)new
            {
                course.Id,
                course.Name,
                course.DurationYears,
                Modules = modules
                    .Where(m => m.CourseId == course.Id)
                    .Select(m => new
                    {
                        m.Id,
                        m.Title,
                        m.Credits,
                        Instructors = moduleInstructors
                            .Where(mi => mi.ModuleId == m.Id)
                            .Select(mi => instructors.FirstOrDefault(i => i.Id == mi.InstructorId))
                            .Where(i => i != null)
                            .Select(i => new { i!.Id, i!.FirstName, i!.LastName, i!.Email })
                            .ToList()
                    }).ToList()
            }).ToList();
        }

        public async Task<int> FetchCoursesCountAsync() => await _dbContext.Course.CountAsync();

        public async Task<int> FetchTotalCreditsAsync() => await _dbContext.Module.SumAsync(m => m.Credits);

        public async Task<List<object>> FetchTopEnrolledCoursesAsync()
        {
            var enrollments = await _dbContext.Enrollment.ToListAsync();
            var courses = await _dbContext.Course.ToListAsync();

            var grouped = enrollments.GroupBy(e => e.CourseId)
                .Select(g => new { CourseId = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .ToList();

            return grouped.Select(g => (object)new
            {
                Course = courses.FirstOrDefault(c => c.Id == g.CourseId),
                EnrollmentCount = g.Count
            }).ToList();
        }
    }
}
