using SecondSemesterProject.Domain.Models;
using System.Collections.Generic;

namespace SecondSemesterProject.Application.Interfaces.IService
{
    public interface IInstructorService
    {
        Task<List<Instructor>> GetAllInstructors();
        Task<Instructor?> GetInstructorById(int id);
        Task CreateInstructor(Instructor instructor);
        Task UpdateInstructor(int id, Instructor instructor);
        Task DeleteInstructor(int id);
        Task BulkInsertInstructors(List<Instructor> instructors);
        Task<object> GetInstructorsWithModules();
        Task<int> GetInstructorCount();
        Task<List<int>> GetDistinctHireYears();
        Task<object> GetModuleCountPerInstructor();
    }
}
