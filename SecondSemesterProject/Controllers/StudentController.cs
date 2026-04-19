using Microsoft.AspNetCore.Mvc;
using SecondSemesterProject.Application.DTO;
using SecondSemesterProject.Application.Interfaces.IService;
using SecondSemesterProject.Domain.Models;

namespace SecondSemesterProject.Controllers
{
    [ApiController]
    [Route("api")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("students")]
        public async Task<List<Student>> GetAllStudentsFromDBAsync()
        {
            return await _studentService.GetAllStudentsAsync();
        }

        [HttpPost("student")]
        public async Task<IActionResult> AddStudent(StudentDTO student)
        {
            await _studentService.AddStudentAsync(student);
            return Ok("Student added successfully!");
        }

        [HttpGet("student/{id}")]
        public async Task<IActionResult> GetStudentByIdAsync(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            return Ok(student);
        }

        [HttpPut("student/{id}")]
        public async Task<IActionResult> UpdateStudentAsync([FromRoute] int id, [FromBody] StudentDTO student)
        {
            await _studentService.UpdateStudentAsync(id, student);
            return Ok("Student updated successfully!");
        }

        [HttpGet("students/{id}/courses")]
        public async Task<IActionResult> GetStudentCoursesAsync([FromRoute] int id)
        {
            var courses = await _studentService.GetStudentCoursesAsync(id);

            if (courses == null)
            {
                return NotFound("Student not found.");
            }

            return Ok(courses);
        }

        [HttpDelete("students/{id}")]
        public async Task<IActionResult> DeleteStudentAsync([FromRoute] int id)
        {
            var deleted = await _studentService.DeleteStudentAsync(id);

            if (!deleted)
            {
                return NotFound("Student not found.");
            }

            return Ok("Student deleted successfully!");
        }

        [HttpPost("students/bulk")]
        public async Task<IActionResult> AddStudentsBulkAsync([FromBody] List<StudentDTO> students)
        {
            await _studentService.AddStudentsBulkAsync(students);
            return Ok("Students added successfully!");
        }

        [HttpGet("students/with-courses")]
        public async Task<IActionResult> GetStudentsWithCoursesAsync()
        {
            var students = await _studentService.GetStudentsWithCoursesAsync();
            return Ok(students);
        }

        [HttpGet("students/count")]
        public async Task<IActionResult> GetStudentsCountAsync()
        {
            var count = await _studentService.GetStudentsCountAsync();
            return Ok(count);
        }

        [HttpGet("students/full-details")]
        public async Task<IActionResult> GetStudentsFullDetailsAsync()
        {
            var students = await _studentService.GetStudentsFullDetailsAsync();
            return Ok(students);
        }
    }
}
