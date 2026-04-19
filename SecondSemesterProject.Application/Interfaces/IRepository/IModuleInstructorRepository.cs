using SecondSemesterProject.Domain.Models;

namespace SecondSemesterProject.Application.Interfaces.IRepository
{
    public interface IModuleInstructorRepository
    {
        Task AssignInstructorToModule(ModuleInstructor assignment);
        Task RemoveInstructorFromModule(int id);
        Task BulkAssign(List<ModuleInstructor> assignments);
        Task<object> GetModuleInstructorFullDetails();
        Task<int> GetTotalAssignments();
        Task<object> GetModuleCountByInstructor();
    }
}
