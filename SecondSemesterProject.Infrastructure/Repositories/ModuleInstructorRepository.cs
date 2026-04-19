using Microsoft.EntityFrameworkCore;
using SecondSemesterProject.Application.Interfaces.IRepository;
using SecondSemesterProject.Domain.Models;
using SecondSemesterProject.Infrastructure.Persistence;


namespace SecondSemesterProject.Infrastructure.Repositories
{
    public class ModuleInstructorRepository : IModuleInstructorRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public ModuleInstructorRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AssignInstructorToModule(ModuleInstructor assignment)
        {
            await _dbContext.ModuleInstructor.AddAsync(assignment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveInstructorFromModule(int id)
        {
            var existing = await _dbContext.ModuleInstructor.FindAsync(id);
            if (existing == null) return;

            _dbContext.ModuleInstructor.Remove(existing);
            await _dbContext.SaveChangesAsync();
        }

        public async Task BulkAssign(List<ModuleInstructor> assignments)
        {
            await _dbContext.ModuleInstructor.AddRangeAsync(assignments);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<object> GetModuleInstructorFullDetails()
        {
            var links = await _dbContext.ModuleInstructor
                .Include(mi => mi.Module)
                .Include(mi => mi.Instructor)
                .ToListAsync();

            return links.Select(mi => (object)new
            {
                mi.Id,
                mi.ModuleId,
                Module = mi.Module == null ? null : new { mi.Module.Id, mi.Module.Title, mi.Module.Credits },
                mi.InstructorId,
                Instructor = mi.Instructor == null ? null : new { mi.Instructor.Id, mi.Instructor.FirstName, mi.Instructor.LastName, mi.Instructor.Email }
            }).ToList();
        }

        public async Task<int> GetTotalAssignments() => await _dbContext.ModuleInstructor.CountAsync();

        public async Task<object> GetModuleCountByInstructor()
        {
            var instructors = await _dbContext.Instructor.ToListAsync();
            var grouped = await _dbContext.ModuleInstructor
                .GroupBy(mi => mi.InstructorId)
                .Select(g => new { InstructorId = g.Key, ModuleCount = g.Count() })
                .ToListAsync();

            return grouped.Select(g => (object)new
            {
                Instructor = instructors.FirstOrDefault(i => i.Id == g.InstructorId),
                g.ModuleCount
            }).ToList();
        }
    }
}
