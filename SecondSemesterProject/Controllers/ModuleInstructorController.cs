using Microsoft.AspNetCore.Mvc;
using SecondSemesterProject.Application.DTO;
using SecondSemesterProject.Application.Interfaces.IService;

namespace SecondSemesterProject.Controllers
{
    [ApiController]
    [Route("api")]
    public class ModuleInstructorController : Controller
    {
        private readonly IModuleInstructorService _moduleInstructorService;

        public ModuleInstructorController(IModuleInstructorService moduleInstructorService)
        {
            _moduleInstructorService = moduleInstructorService;
        }

        [HttpPost("module-instructors")]
        public async Task<IActionResult> AssignInstructorToModule([FromBody] ModuleInstructorDTO assignment)
        {
            await _moduleInstructorService.AssignInstructorToModule(assignment);
            return Ok("Instructor assigned to module successfully.");
        }

        [HttpDelete("module-instructors/{id}")]
        public async Task<IActionResult> RemoveInstructorFromModule(int id)
        {
            await _moduleInstructorService.RemoveInstructorFromModule(id);
            return Ok("Assignment removed successfully.");
        }

        [HttpPost("module-instructors/bulk")]
        public async Task<IActionResult> BulkAssign([FromBody] List<ModuleInstructorDTO> assignments)
        {
            await _moduleInstructorService.BulkAssign(assignments);
            return Ok("Bulk assignment completed.");
        }

        [HttpGet("module-instructors/full-details")]
        public async Task<IActionResult> GetModuleInstructorFullDetails()
        {
            var details = await _moduleInstructorService.GetModuleInstructorFullDetails();
            return Ok(details);
        }

        [HttpGet("module-instructors/count")]
        public async Task<IActionResult> GetTotalAssignments()
        {
            var count = await _moduleInstructorService.GetTotalAssignments();
            return Ok(count);
        }

        [HttpGet("module-instructors/module-count-by-instructor")]
        public async Task<IActionResult> GetModuleCountByInstructor()
        {
            var data = await _moduleInstructorService.GetModuleCountByInstructor();
            return Ok(data);
        }
    }
}
