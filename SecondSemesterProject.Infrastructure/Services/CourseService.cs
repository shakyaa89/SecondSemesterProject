using SecondSemesterProject.Application.DTO;
using SecondSemesterProject.Application.Interfaces.IRepository;
using SecondSemesterProject.Application.Interfaces.IService;
using SecondSemesterProject.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace SecondSemesterProject.Infrastructure.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }


        public async Task<List<object>> GetAllCoursesWithModuleCountAsync() => await _courseRepository.FetchAllCoursesWithModuleCountAsync();

        public async Task<Course?> GetCourseByIdAsync(int id) => await _courseRepository.FetchCourseByIdAsync(id);

        public async Task<object?> GetCourseWithModulesAsync(int id) => await _courseRepository.FetchCourseWithModulesAsync(id);

        public async Task<List<Module>> GetCourseModulesAsync(int id) => await _courseRepository.FetchCourseModulesAsync(id);

        public async Task<List<Student>> GetCourseStudentsAsync(int id) => await _courseRepository.FetchCourseStudentsAsync(id);

        public async Task AddCourseAsync(CourseDTO courseDto)
        {
            var course = new Course { Name = courseDto.Name, DurationYears = courseDto.DurationYears };

            List<Module>? modules = null;
            if (courseDto.Modules != null && courseDto.Modules.Any())
            {
                modules = courseDto.Modules.Select(m => new Module { Title = m.Title, Credits = m.Credits }).ToList();
            }

            await _courseRepository.AddCourseAsync(course, modules);
        }

        public async Task AddModuleToCourseAsync(int courseId, ModuleDTO moduleDto)
        {
            var module = new Module { Title = moduleDto.Title, Credits = moduleDto.Credits, CourseId = courseId };
            await _courseRepository.AddModuleToCourseAsync(courseId, module);
        }

        public async Task UpdateCourseAsync(int id, CourseDTO courseDto)
        {
            var course = new Course { Name = courseDto.Name, DurationYears = courseDto.DurationYears };

            List<Module>? modules = null;
            if (courseDto.Modules != null && courseDto.Modules.Any())
            {
                modules = courseDto.Modules.Select(m => new Module { Title = m.Title, Credits = m.Credits }).ToList();
            }

            await _courseRepository.UpdateCourseAsync(id, course, modules);
        }

        public async Task<bool> DeleteCourseAsync(int id) => await _courseRepository.DeleteCourseAsync(id);

        public async Task AddCoursesBulkAsync(List<CourseDTO> courses)
        {
            var newCourses = courses.Select(c => new Course { Name = c.Name, DurationYears = c.DurationYears }).ToList();
            await _courseRepository.AddCoursesAsync(newCourses);
        }

        public async Task<List<object>> GetCoursesWithDetailsAsync() => await _courseRepository.FetchCoursesWithDetailsAsync();

        public async Task<int> GetCoursesCountAsync() => await _courseRepository.FetchCoursesCountAsync();

        public async Task<int> GetTotalCreditsAsync() => await _courseRepository.FetchTotalCreditsAsync();

        public async Task<List<object>> GetTopEnrolledCoursesAsync() => await _courseRepository.FetchTopEnrolledCoursesAsync();
    }
}
