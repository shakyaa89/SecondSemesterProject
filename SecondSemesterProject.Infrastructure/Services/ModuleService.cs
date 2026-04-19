using SecondSemesterProject.Application.DTO;
using SecondSemesterProject.Application.Interfaces.IRepository;
using SecondSemesterProject.Application.Interfaces.IService;
using SecondSemesterProject.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace SecondSemesterProject.Infrastructure.Services
{
    public class ModuleService : IModuleService
    {
        private readonly IModuleRepository _moduleRepository;

        public ModuleService(IModuleRepository moduleRepository)
        {
            _moduleRepository = moduleRepository;
        }

        public async Task<List<Module>> GetAllModulesAsync() => await _moduleRepository.FetchAllModulesAsync();

        public async Task<Module?> GetModuleByIdAsync(int id) => await _moduleRepository.FetchModuleByIdAsync(id);

        public async Task<List<Instructor>> GetModuleInstructorsAsync(int moduleId) => await _moduleRepository.FetchModuleInstructorsAsync(moduleId);

        public async Task AddModuleAsync(ModuleDTO moduleDto)
        {
            var module = new Module { Title = moduleDto.Title, Credits = moduleDto.Credits, CourseId = moduleDto.CourseId };
            await _moduleRepository.AddModuleAsync(module);
        }

        public async Task UpdateModuleAsync(int id, ModuleDTO moduleDto)
        {
            var module = new Module { Title = moduleDto.Title, Credits = moduleDto.Credits, CourseId = moduleDto.CourseId };
            await _moduleRepository.UpdateModuleAsync(id, module);
        }

        public async Task<bool> DeleteModuleAsync(int id) => await _moduleRepository.DeleteModuleAsync(id);

        public async Task AddModulesBulkAsync(List<ModuleDTO> modules)
        {
            var newModules = modules.Select(m => new Module { Title = m.Title, Credits = m.Credits, CourseId = m.CourseId }).ToList();
            await _moduleRepository.AddModulesAsync(newModules);
        }

        public async Task<List<object>> GetModulesWithCourseAsync() => await _moduleRepository.FetchModulesWithCourseAsync();

        public async Task<int> GetModulesCountAsync() => await _moduleRepository.FetchModulesCountAsync();

        public async Task<List<Module>> GetHighCreditModulesAsync(int minCredits) => await _moduleRepository.FetchHighCreditModulesAsync(minCredits);

        public async Task UpdateModulesCreditsAsync(List<ModuleDTO> updates)
        {
            var list = updates.Select(u => (u.Id, u.Credits)).ToList();
            await _moduleRepository.UpdateModulesCreditsAsync(list);
        }
    }
}
