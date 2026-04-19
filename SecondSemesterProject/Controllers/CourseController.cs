using Microsoft.AspNetCore.Mvc;
using SecondSemesterProject.Application.DTO;
using SecondSemesterProject.Application.Interfaces.IService;
using SecondSemesterProject.Domain.Models;

namespace SecondSemesterProject.Controllers
{
    [ApiController]
    [Route("api")]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        
        [HttpGet("courses")]
        public async Task<IActionResult> GetAllCoursesWithModuleCountAsync()
        {
            var list = await _courseService.GetAllCoursesWithModuleCountAsync();
            return Ok(list);
        }

        [HttpGet("courses/{id}")]
        public async Task<IActionResult> GetCourseWithModulesAsync(int id)
        {
            var course = await _courseService.GetCourseWithModulesAsync(id);
            if (course == null) return NotFound("Course not found.");
            return Ok(course);
        }

        [HttpGet("courses/{id}/modules")]
        public async Task<IActionResult> GetCourseModulesAsync(int id)
        {
            var modules = await _courseService.GetCourseModulesAsync(id);
            return Ok(modules);
        }

        [HttpGet("courses/{id}/students")]
        public async Task<IActionResult> GetCourseStudentsAsync(int id)
        {
            var students = await _courseService.GetCourseStudentsAsync(id);
            return Ok(students);
        }

        [HttpPost("courses")]
        public async Task<IActionResult> CreateCourseAsync([FromBody] CourseDTO course)
        {
            await _courseService.AddCourseAsync(course);
            return Ok("Course created successfully.");
        }

        [HttpPost("courses/{id}/modules")]
        public async Task<IActionResult> AddModuleToCourseAsync(int id, [FromBody] ModuleDTO module)
        {
            await _courseService.AddModuleToCourseAsync(id, module);
            return Ok("Module added to course.");
        }

        [HttpPut("courses/{id}")]
        public async Task<IActionResult> UpdateCourseAsync(int id, [FromBody] CourseDTO course)
        {
            await _courseService.UpdateCourseAsync(id, course);
            return Ok("Course updated successfully.");
        }

        [HttpDelete("courses/{id}")]
        public async Task<IActionResult> DeleteCourseAsync(int id)
        {
            var deleted = await _courseService.DeleteCourseAsync(id);
            if (!deleted) return NotFound("Course not found.");
            return Ok("Course deleted successfully.");
        }

        [HttpPost("courses/bulk")]
        public async Task<IActionResult> BulkInsertCoursesAsync([FromBody] List<CourseDTO> courses)
        {
            await _courseService.AddCoursesBulkAsync(courses);
            return Ok("Courses added successfully.");
        }

        [HttpGet("courses/with-details")]
        public async Task<IActionResult> GetCoursesWithDetailsAsync()
        {
            var details = await _courseService.GetCoursesWithDetailsAsync();
            return Ok(details);
        }

        [HttpGet("courses/count")]
        public async Task<IActionResult> GetCoursesCountAsync()
        {
            var count = await _courseService.GetCoursesCountAsync();
            return Ok(count);
        }

        [HttpGet("courses/total-credits")]
        public async Task<IActionResult> GetTotalCreditsAsync()
        {
            var sum = await _courseService.GetTotalCreditsAsync();
            return Ok(sum);
        }

        [HttpGet("courses/top-enrolled")]
        public async Task<IActionResult> GetTopEnrolledAsync()
        {
            var list = await _courseService.GetTopEnrolledCoursesAsync();
            return Ok(list);
        }
    }
}
