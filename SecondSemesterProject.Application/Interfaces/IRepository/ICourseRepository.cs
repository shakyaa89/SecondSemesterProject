using SecondSemesterProject.Domain.Models;
using System.Collections.Generic;

namespace SecondSemesterProject.Application.Interfaces.IRepository
{
    public interface ICourseRepository
    {
        Task<List<object>> FetchAllCoursesWithModuleCountAsync();
        Task<Course?> FetchCourseByIdAsync(int id);
        Task<object?> FetchCourseWithModulesAsync(int id);
        Task<List<Module>> FetchCourseModulesAsync(int id);
        Task<List<Student>> FetchCourseStudentsAsync(int id);
        Task AddCourseAsync(Course course, List<Module>? modules = null);
        Task AddModuleToCourseAsync(int courseId, Module module);
        Task UpdateCourseAsync(int id, Course course, List<Module>? modules = null);
        Task<bool> DeleteCourseAsync(int id);
        Task AddCoursesAsync(List<Course> courses);
        Task<List<object>> FetchCoursesWithDetailsAsync();
        Task<int> FetchCoursesCountAsync();
        Task<int> FetchTotalCreditsAsync();
        Task<List<object>> FetchTopEnrolledCoursesAsync();
    }
}
