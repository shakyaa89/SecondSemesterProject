using SecondSemesterProject.Application.DTO;

namespace SecondSemesterProject.Application.Interfaces.IService
{
    public interface IModuleInstructorService
    {
        Task AssignInstructorToModule(ModuleInstructorDTO assignment);
        Task RemoveInstructorFromModule(int id);
        Task BulkAssign(List<ModuleInstructorDTO> assignments);
        Task<object> GetModuleInstructorFullDetails();
        Task<int> GetTotalAssignments();
        Task<object> GetModuleCountByInstructor();
    }
}
