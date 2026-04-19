using SecondSemesterProject.Application.DTO;
using SecondSemesterProject.Domain.Models;
using System.Collections.Generic;

namespace SecondSemesterProject.Application.Interfaces.IService
{
    public interface ICourseService
    {
        Task<List<object>> GetAllCoursesWithModuleCountAsync();
        Task<Course?> GetCourseByIdAsync(int id);
        Task<object?> GetCourseWithModulesAsync(int id);
        Task<List<Module>> GetCourseModulesAsync(int id);
        Task<List<Student>> GetCourseStudentsAsync(int id);
        Task AddCourseAsync(CourseDTO courseDto);
        Task AddModuleToCourseAsync(int courseId, ModuleDTO moduleDto);
        Task UpdateCourseAsync(int id, CourseDTO courseDto);
        Task<bool> DeleteCourseAsync(int id);
        Task AddCoursesBulkAsync(List<CourseDTO> courses);
        Task<List<object>> GetCoursesWithDetailsAsync();
        Task<int> GetCoursesCountAsync();
        Task<int> GetTotalCreditsAsync();
        Task<List<object>> GetTopEnrolledCoursesAsync();
    }
}
