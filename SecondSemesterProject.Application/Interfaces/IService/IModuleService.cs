using SecondSemesterProject.Application.DTO;
using SecondSemesterProject.Domain.Models;
using System.Collections.Generic;

namespace SecondSemesterProject.Application.Interfaces.IService
{
    public interface IModuleService
    {
        Task<List<Module>> GetAllModulesAsync();
        Task<Module?> GetModuleByIdAsync(int id);
        Task<List<Instructor>> GetModuleInstructorsAsync(int moduleId);
        Task AddModuleAsync(ModuleDTO moduleDto);
        Task UpdateModuleAsync(int id, ModuleDTO moduleDto);
        Task<bool> DeleteModuleAsync(int id);
        Task AddModulesBulkAsync(List<ModuleDTO> modules);
        Task<List<object>> GetModulesWithCourseAsync();
        Task<int> GetModulesCountAsync();
        Task<List<Module>> GetHighCreditModulesAsync(int minCredits);
        Task UpdateModulesCreditsAsync(List<ModuleDTO> updates);
    }
}
