using SecondSemesterProject.DTO;
using SecondSemesterProject.Models;
using SecondSemesterProject.Repositories;

namespace SecondSemesterProject.Services
{
    public class StudentService: IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }


        public async Task<List<Student>> GetAllStudentsAsync() => await _studentRepository.FetchAllStudentsAsync();

        public async Task AddStudentAsync(StudentDTO student)
        {
            Student newStudent = new() { FirstName = student.FirstName, LastName = student.LastName, DateOfBirth = student.DateOfBirth, Phone = student.Phone, Email = student.Email };

            await _studentRepository.AddStudentAsync(newStudent);
        }

        public async Task<Student?> GetStudentByIdAsync(int studentId)
        {
            return await _studentRepository.FetchStudentByIdAsync(studentId);
        } 

        public async Task UpdateStudentAsync(int id, StudentDTO student)
        {
            Student newStudent = new() { FirstName = student.FirstName, LastName = student.LastName, DateOfBirth = student.DateOfBirth, Email = student.Email, Phone = student.Phone };

            await _studentRepository.UpdateStudentAsync(id, newStudent);
        }

        public async Task<List<object>?> GetStudentCoursesAsync(int id)
        {
            var student = await _studentRepository.FetchStudentByIdAsync(id);

            if (student == null)
            {
                return null;
            }

            var courses = await _studentRepository.FetchStudentCoursesAsync(id);

            return courses
                .Select(c => (object)new { c.Id, c.Name, c.DurationYears })
                .ToList();
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            return await _studentRepository.DeleteStudentAsync(id);
        }

        public async Task AddStudentsBulkAsync(List<StudentDTO> students)
        {
            var newStudents = students.Select(student => new Student
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                DateOfBirth = student.DateOfBirth,
                Phone = student.Phone,
                Email = student.Email
            }).ToList();

            await _studentRepository.AddStudentsAsync(newStudents);
        }

        public async Task<List<object>> GetStudentsWithCoursesAsync()
        {
            return await _studentRepository.FetchAllStudentsWithCoursesAsync();
        }

        public async Task<int> GetStudentsCountAsync()
        {
            return await _studentRepository.FetchStudentsCountAsync();
        }

        public async Task<List<object>> GetStudentsFullDetailsAsync()
        {
            return await _studentRepository.FetchStudentsFullDetailsAsync();
        }

    }
}
