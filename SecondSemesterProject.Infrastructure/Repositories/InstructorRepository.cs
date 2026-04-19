using Microsoft.EntityFrameworkCore;
using SecondSemesterProject.Domain.Models;
using SecondSemesterProject.Infrastructure.Persistence;
using SecondSemesterProject.Application.Interfaces.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace SecondSemesterProject.Infrastructure.Repositories
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public InstructorRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Instructor>> GetAllInstructors() => await _dbContext.Instructor.ToListAsync();

        public async Task<Instructor?> GetInstructorById(int id) => await _dbContext.Instructor.FindAsync(id);

        public async Task CreateInstructor(Instructor instructor)
        {
            await _dbContext.Instructor.AddAsync(instructor);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateInstructor(int id, Instructor instructor)
        {
            var existing = await _dbContext.Instructor.FindAsync(id);
            if (existing == null) return;

            existing.FirstName = instructor.FirstName;
            existing.LastName = instructor.LastName;
            existing.Email = instructor.Email;
            existing.HireDate = instructor.HireDate;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteInstructor(int id)
        {
            var existing = await _dbContext.Instructor.FindAsync(id);
            if (existing == null) return;

            // Remove any ModuleInstructor links
            var moduleLinks = await _dbContext.ModuleInstructor.Where(mi => mi.InstructorId == id).ToListAsync();
            if (moduleLinks.Any()) _dbContext.ModuleInstructor.RemoveRange(moduleLinks);

            _dbContext.Instructor.Remove(existing);
            await _dbContext.SaveChangesAsync();
        }

        public async Task BulkInsertInstructors(List<Instructor> instructors)
        {
            await _dbContext.Instructor.AddRangeAsync(instructors);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<object> GetInstructorsWithModules()
        {
            var instructors = await _dbContext.Instructor.ToListAsync();
            var moduleInstructors = await _dbContext.ModuleInstructor.Include(mi => mi.Module).ToListAsync();

            var result = instructors.Select(i => (object)new
            {
                i.Id,
                i.FirstName,
                i.LastName,
                i.Email,
                i.HireDate,
                Modules = moduleInstructors
                    .Where(mi => mi.InstructorId == i.Id)
                    .Where(mi => mi.Module != null)
                    .Select(mi => new { mi.Module!.Id, mi.Module!.Title, mi.Module!.Credits })
                    .ToList()
            }).ToList();

            return result;
        }

        public async Task<int> GetInstructorCount() => await _dbContext.Instructor.CountAsync();

        public async Task<List<int>> GetDistinctHireYears()
        {
            var hireStrings = await _dbContext.Instructor.Select(i => i.HireDate).ToListAsync();
            var years = hireStrings
                .Select(h => {
                    if (int.TryParse(h?.Split('-').FirstOrDefault() ?? h, out var y)) return (int?)y;
                    if (h != null && h.Length >= 4 && int.TryParse(h.Substring(0,4), out var y2)) return (int?)y2;
                    return null;
                })
                .Where(y => y.HasValue)
                .Select(y => y!.Value)
                .Distinct()
                .OrderByDescending(y => y)
                .ToList();

            return years;
        }

        public async Task<object> GetModuleCountPerInstructor()
        {
            var moduleInstructors = await _dbContext.ModuleInstructor.ToListAsync();
            var instructors = await _dbContext.Instructor.ToListAsync();

            var grouped = moduleInstructors.GroupBy(mi => mi.InstructorId)
                .Select(g => new
                {
                    InstructorId = g.Key,
                    ModuleCount = g.Count()
                }).ToList();

            var result = grouped.Select(g => (object)new
            {
                Instructor = instructors.FirstOrDefault(i => i.Id == g.InstructorId),
                g.ModuleCount
            }).ToList();

            return result;
        }
    }
}
