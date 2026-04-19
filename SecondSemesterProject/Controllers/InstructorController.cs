using Microsoft.AspNetCore.Mvc;
using SecondSemesterProject.Application.DTO;
using SecondSemesterProject.Application.Interfaces.IService;
using SecondSemesterProject.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace SecondSemesterProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorService _instructorService;

        public InstructorController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Instructor>>> GetAll()
        {
            var list = await _instructorService.GetAllInstructors();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Instructor>> GetById(int id)
        {
            var inst = await _instructorService.GetInstructorById(id);
            if (inst == null) return NotFound();
            return Ok(inst);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InstructorDTO dto)
        {
            var inst = new Instructor { FirstName = dto.FirstName, LastName = dto.LastName, Email = dto.Email, HireDate = dto.HireDate };
            await _instructorService.CreateInstructor(inst);
            return CreatedAtAction(nameof(GetById), new { id = inst.Id }, inst);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] InstructorDTO dto)
        {
            var existing = await _instructorService.GetInstructorById(id);
            if (existing == null) return NotFound();

            existing.FirstName = dto.FirstName;
            existing.LastName = dto.LastName;
            existing.Email = dto.Email;
            existing.HireDate = dto.HireDate;

            await _instructorService.UpdateInstructor(id, existing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _instructorService.GetInstructorById(id);
            if (existing == null) return NotFound();

            await _instructorService.DeleteInstructor(id);
            return NoContent();
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> BulkInsert([FromBody] List<InstructorDTO> dtos)
        {
            var list = dtos.Select(d => new Instructor { FirstName = d.FirstName, LastName = d.LastName, Email = d.Email, HireDate = d.HireDate }).ToList();
            await _instructorService.BulkInsertInstructors(list);
            return Accepted();
        }

        [HttpGet("with-modules")]
        public async Task<IActionResult> WithModules()
        {
            var res = await _instructorService.GetInstructorsWithModules();
            return Ok(res);
        }

        [HttpGet("count")]
        public async Task<IActionResult> Count() => Ok(await _instructorService.GetInstructorCount());

        [HttpGet("hire-years")]
        public async Task<IActionResult> HireYears() => Ok(await _instructorService.GetDistinctHireYears());

        [HttpGet("module-count")]
        public async Task<IActionResult> ModuleCount() => Ok(await _instructorService.GetModuleCountPerInstructor());
    }
}
