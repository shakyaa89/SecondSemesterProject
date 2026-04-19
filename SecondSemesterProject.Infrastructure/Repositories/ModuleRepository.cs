using Microsoft.EntityFrameworkCore;
using SecondSemesterProject.Domain.Models;
using SecondSemesterProject.Infrastructure.Persistence;
using SecondSemesterProject.Application.Interfaces.IRepository;
using System.Linq;

namespace SecondSemesterProject.Infrastructure.Repositories
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public ModuleRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Module>> FetchAllModulesAsync() => await _dbContext.Module.ToListAsync();

        public async Task<Module?> FetchModuleByIdAsync(int id) => await _dbContext.Module.FindAsync(id);

        public async Task<List<Instructor>> FetchModuleInstructorsAsync(int moduleId)
        {
            var mis = await _dbContext.ModuleInstructor
                .Where(mi => mi.ModuleId == moduleId)
                .Include(mi => mi.Instructor)
                .ToListAsync();

            return mis.Where(mi => mi.Instructor != null).Select(mi => mi.Instructor!).GroupBy(i => i.Id).Select(g => g.First()).ToList();
        }

        public async Task AddModuleAsync(Module module)
        {
            await _dbContext.Module.AddAsync(module);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateModuleAsync(int id, Module module)
        {
            var existing = await _dbContext.Module.FindAsync(id);
            if (existing == null) return;

            existing.Title = module.Title;
            existing.Credits = module.Credits;
            existing.CourseId = module.CourseId;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteModuleAsync(int id)
        {
            var existing = await _dbContext.Module.FindAsync(id);
            if (existing == null) return false;

            var moduleInstructors = await _dbContext.ModuleInstructor.Where(mi => mi.ModuleId == id).ToListAsync();
            if (moduleInstructors.Any()) _dbContext.ModuleInstructor.RemoveRange(moduleInstructors);

            _dbContext.Module.Remove(existing);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task AddModulesAsync(List<Module> modules)
        {
            await _dbContext.Module.AddRangeAsync(modules);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<object>> FetchModulesWithCourseAsync()
        {
            var modules = await _dbContext.Module.ToListAsync();
            var courses = await _dbContext.Course.ToListAsync();

            return modules.Select(m => (object)new
            {
                m.Id,
                m.Title,
                m.Credits,
                Course = courses.FirstOrDefault(c => c.Id == m.CourseId) is var c && c != null ? new { c.Id, c.Name, c.DurationYears } : null
            }).ToList();
        }

        public async Task<int> FetchModulesCountAsync() => await _dbContext.Module.CountAsync();

        public async Task<List<Module>> FetchHighCreditModulesAsync(int minCredits) => await _dbContext.Module.Where(m => m.Credits > minCredits).ToListAsync();

        public async Task UpdateModulesCreditsAsync(List<(int Id, int Credits)> updates)
        {
            var ids = updates.Select(u => u.Id).ToList();
            var existing = await _dbContext.Module.Where(m => ids.Contains(m.Id)).ToListAsync();

            foreach (var mod in existing)
            {
                var upd = updates.FirstOrDefault(u => u.Id == mod.Id);
                mod.Credits = upd.Credits;
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
