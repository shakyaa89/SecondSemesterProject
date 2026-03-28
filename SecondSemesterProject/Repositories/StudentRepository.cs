using Microsoft.EntityFrameworkCore;
using SecondSemesterProject.Models;
using SecondSemesterProject.Persistance;

namespace SecondSemesterProject.Repositories
{
    public class StudentRepository: IStudentRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public StudentRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Student>> FetchAllStudentsAsync() => await _dbContext.Student.ToListAsync();

        public async Task AddStudentAsync(Student student)
        {
            await _dbContext.Student.AddAsync(student);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task<Student?> FetchStudentByIdAsync(int studentId) => await _dbContext.Student.FindAsync(studentId);

        public async Task UpdateStudentAsync(int id, Student student)
        {
            var existingStudent = await _dbContext.Student.FindAsync(id);

            if (existingStudent == null)
            {
                return;
            }

            System.Diagnostics.Debug.WriteLine(student.LastName);

            existingStudent.FirstName = student.FirstName;
            existingStudent.LastName = student.LastName;
            existingStudent.DateOfBirth = student.DateOfBirth;
            existingStudent.Phone = student.Phone;
            existingStudent.Email = student.Email;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Course>> FetchStudentCoursesAsync(int studentId)
        {
            return await _dbContext.Enrollment
                .AsNoTracking()
                .Where(e => e.StudentId == studentId)
                .Include(e => e.Course)
                .Where(e => e.Course != null)
                .Select(e => e.Course!)
                .GroupBy(c => c.Id)
                .Select(g => g.First())
                .ToListAsync();
        }

        public async Task<bool> DeleteStudentAsync(int studentId)
        {
            var existingStudent = await _dbContext.Student.FindAsync(studentId);

            if (existingStudent == null)
            {
                return false;
            }

            _dbContext.Student.Remove(existingStudent);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task AddStudentsAsync(List<Student> students)
        {
            await _dbContext.Student.AddRangeAsync(students);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<object>> FetchAllStudentsWithCoursesAsync()
        {
            var students = await _dbContext.Student
                .AsNoTracking()
                .ToListAsync();

            var enrollments = await _dbContext.Enrollment
                .AsNoTracking()
                .Include(e => e.Course)
                .Where(e => e.Course != null)
                .ToListAsync();

            return students.Select(student => (object)new
            {
                student.Id,
                student.FirstName,
                student.LastName,
                student.DateOfBirth,
                student.Phone,
                student.Email,
                Courses = enrollments
                    .Where(e => e.StudentId == student.Id)
                    .Select(e => e.Course!)
                    .GroupBy(c => c.Id)
                    .Select(g => g.First())
                    .Select(c => new
                    {
                        c.Id,
                        c.Name,
                        c.DurationYears
                    }).ToList()
            }).ToList();
        }

        public async Task<int> FetchStudentsCountAsync()
        {
            return await _dbContext.Student.CountAsync();
        }

        public async Task<List<object>> FetchStudentsFullDetailsAsync()
        {
            var students = await _dbContext.Student
                .AsNoTracking()
                .ToListAsync();

            var enrollments = await _dbContext.Enrollment
                .AsNoTracking()
                .Include(e => e.Course)
                .Where(e => e.Course != null)
                .ToListAsync();

            var modules = await _dbContext.Module
                .AsNoTracking()
                .ToListAsync();

            return students.Select(student => (object)new
            {
                student.Id,
                student.FirstName,
                student.LastName,
                student.DateOfBirth,
                student.Phone,
                student.Email,
                Courses = enrollments
                    .Where(e => e.StudentId == student.Id)
                    .Select(e => e.Course!)
                    .GroupBy(c => c.Id)
                    .Select(g => g.First())
                    .Select(course => new
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
                                m.Credits
                            }).ToList()
                    }).ToList()
            }).ToList();
        }
    } 
}
