using Microsoft.AspNetCore.Mvc;
using SecondSemesterProject.Application.DTO;
using SecondSemesterProject.Application.Interfaces.IService;

namespace SecondSemesterProject.Controllers
{
    [ApiController]
    [Route("api")]
    public class EnrollmentController : Controller
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpGet("enrollments")]
        public async Task<IActionResult> GetAllEnrollments()
        {
            var list = await _enrollmentService.GetAllEnrollments();
            return Ok(list);
        }

        [HttpPost("enrollments")]
        public async Task<IActionResult> EnrollStudent([FromBody] EnrollmentDTO enrollment)
        {
            await _enrollmentService.EnrollStudent(enrollment);
            return Ok("Enrollment created successfully.");
        }

        [HttpDelete("enrollments/{id}")]
        public async Task<IActionResult> RemoveEnrollment(int id)
        {
            await _enrollmentService.RemoveEnrollment(id);
            return Ok("Enrollment removed successfully.");
        }

        [HttpPost("enrollments/bulk")]
        public async Task<IActionResult> BulkEnroll([FromBody] List<EnrollmentDTO> enrollments)
        {
            await _enrollmentService.BulkEnroll(enrollments);
            return Ok("Bulk enrollment completed.");
        }

        [HttpGet("enrollments/full-details")]
        public async Task<IActionResult> GetEnrollmentFullDetails()
        {
            var details = await _enrollmentService.GetEnrollmentFullDetails();
            return Ok(details);
        }

        [HttpGet("enrollments/count")]
        public async Task<IActionResult> GetEnrollmentCount()
        {
            var count = await _enrollmentService.GetEnrollmentCount();
            return Ok(count);
        }

        [HttpGet("enrollments/by-date")]
        public async Task<IActionResult> GetEnrollmentsByDate([FromQuery] DateTime date)
        {
            var list = await _enrollmentService.GetEnrollmentsByDate(date);
            return Ok(list);
        }
    }
}
