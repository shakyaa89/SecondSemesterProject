using Microsoft.AspNetCore.Mvc;
using SecondSemesterProject.Application.DTO;
using SecondSemesterProject.Application.Interfaces.IService;
using SecondSemesterProject.Domain.Models;
using System.Collections.Generic;

namespace SecondSemesterProject.Controllers
{
    [ApiController]
    [Route("api")]
    public class ModuleController : Controller
    {
        private readonly IModuleService _moduleService;

        public ModuleController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        [HttpGet("modules")]
        public async Task<List<Module>> GetAllModulesAsync() => await _moduleService.GetAllModulesAsync();

        [HttpGet("modules/{id}")]
        public async Task<IActionResult> GetModuleByIdAsync(int id)
        {
            var m = await _moduleService.GetModuleByIdAsync(id);
            if (m == null) return NotFound("Module not found.");
            return Ok(m);
        }

        [HttpGet("modules/{id}/instructors")]
        public async Task<IActionResult> GetModuleInstructorsAsync(int id)
        {
            var list = await _moduleService.GetModuleInstructorsAsync(id);
            return Ok(list);
        }

        [HttpPost("modules")]
        public async Task<IActionResult> CreateModuleAsync([FromBody] ModuleDTO module)
        {
            await _moduleService.AddModuleAsync(module);
            return Ok("Module created successfully.");
        }

        [HttpPut("modules/{id}")]
        public async Task<IActionResult> UpdateModuleAsync(int id, [FromBody] ModuleDTO module)
        {
            await _moduleService.UpdateModuleAsync(id, module);
            return Ok("Module updated successfully.");
        }

        [HttpDelete("modules/{id}")]
        public async Task<IActionResult> DeleteModuleAsync(int id)
        {
            var deleted = await _moduleService.DeleteModuleAsync(id);
            if (!deleted) return NotFound("Module not found.");
            return Ok("Module deleted successfully.");
        }

        [HttpPost("modules/bulk")]
        public async Task<IActionResult> BulkInsertModulesAsync([FromBody] List<ModuleDTO> modules)
        {
            await _moduleService.AddModulesBulkAsync(modules);
            return Ok("Modules added successfully.");
        }

        [HttpGet("modules/with-course")]
        public async Task<IActionResult> GetModulesWithCourseAsync()
        {
            var list = await _moduleService.GetModulesWithCourseAsync();
            return Ok(list);
        }

        [HttpGet("modules/count")]
        public async Task<IActionResult> GetModulesCountAsync()
        {
            var count = await _moduleService.GetModulesCountAsync();
            return Ok(count);
        }

        [HttpGet("modules/high-credit")]
        public async Task<IActionResult> GetHighCreditModulesAsync([FromQuery] int minCredits = 10)
        {
            var list = await _moduleService.GetHighCreditModulesAsync(minCredits);
            return Ok(list);
        }

        [HttpPut("modules/bulk-update-credits")]
        public async Task<IActionResult> BulkUpdateCreditsAsync([FromBody] List<ModuleDTO> updates)
        {
            await _moduleService.UpdateModulesCreditsAsync(updates);
            return Ok("Modules credits updated.");
        }
    }
}
