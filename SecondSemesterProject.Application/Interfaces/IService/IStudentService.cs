using SecondSemesterProject.Application.DTO;
using SecondSemesterProject.Domain.Models;

namespace SecondSemesterProject.Application.Interfaces.IService
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllStudentsAsync();

        Task AddStudentAsync(StudentDTO student);

        Task<Student?> GetStudentByIdAsync(int id);

        Task UpdateStudentAsync(int id, StudentDTO student);

        Task<List<object>?> GetStudentCoursesAsync(int id);

        Task<bool> DeleteStudentAsync(int id);

        Task AddStudentsBulkAsync(List<StudentDTO> students);

        Task<List<object>> GetStudentsWithCoursesAsync();

        Task<int> GetStudentsCountAsync();

        Task<List<object>> GetStudentsFullDetailsAsync();
    }
}
