using SecondSemesterProject.Domain.Models;
using System.Collections.Generic;

namespace SecondSemesterProject.Application.Interfaces.IRepository
{
    public interface IModuleRepository
    {
        Task<List<Module>> FetchAllModulesAsync();
        Task<Module?> FetchModuleByIdAsync(int id);
        Task<List<Instructor>> FetchModuleInstructorsAsync(int moduleId);
        Task AddModuleAsync(Module module);
        Task UpdateModuleAsync(int id, Module module);
        Task<bool> DeleteModuleAsync(int id);
        Task AddModulesAsync(List<Module> modules);
        Task<List<object>> FetchModulesWithCourseAsync();
        Task<int> FetchModulesCountAsync();
        Task<List<Module>> FetchHighCreditModulesAsync(int minCredits);
        Task UpdateModulesCreditsAsync(List<(int Id, int Credits)> updates);
    }
}
