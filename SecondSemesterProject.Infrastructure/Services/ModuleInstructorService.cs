using SecondSemesterProject.Application.DTO;
using SecondSemesterProject.Application.Interfaces.IRepository;
using SecondSemesterProject.Application.Interfaces.IService;
using SecondSemesterProject.Domain.Models;

namespace SecondSemesterProject.Infrastructure.Services
{
    public class ModuleInstructorService : IModuleInstructorService
    {
        private readonly IModuleInstructorRepository _moduleInstructorRepository;

        public ModuleInstructorService(IModuleInstructorRepository moduleInstructorRepository)
        {
            _moduleInstructorRepository = moduleInstructorRepository;
        }

        public async Task AssignInstructorToModule(ModuleInstructorDTO assignment)
        {
            var model = new ModuleInstructor
            {
                ModuleId = assignment.ModuleId,
                InstructorId = assignment.InstructorId
            };

            await _moduleInstructorRepository.AssignInstructorToModule(model);
        }

        public async Task RemoveInstructorFromModule(int id) => await _moduleInstructorRepository.RemoveInstructorFromModule(id);

        public async Task BulkAssign(List<ModuleInstructorDTO> assignments)
        {
            var models = assignments.Select(a => new ModuleInstructor
            {
                ModuleId = a.ModuleId,
                InstructorId = a.InstructorId
            }).ToList();

            await _moduleInstructorRepository.BulkAssign(models);
        }

        public async Task<object> GetModuleInstructorFullDetails() => await _moduleInstructorRepository.GetModuleInstructorFullDetails();

        public async Task<int> GetTotalAssignments() => await _moduleInstructorRepository.GetTotalAssignments();

        public async Task<object> GetModuleCountByInstructor() => await _moduleInstructorRepository.GetModuleCountByInstructor();
    }
}
