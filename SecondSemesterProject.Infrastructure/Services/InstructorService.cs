using SecondSemesterProject.Application.Interfaces.IRepository;
using SecondSemesterProject.Application.Interfaces.IService;
using SecondSemesterProject.Domain.Models;
using System.Collections.Generic;

namespace SecondSemesterProject.Infrastructure.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly IInstructorRepository _instructorRepository;

        public InstructorService(IInstructorRepository instructorRepository)
        {
            _instructorRepository = instructorRepository;
        }

        public async Task<List<Instructor>> GetAllInstructors() => await _instructorRepository.GetAllInstructors();

        public async Task<Instructor?> GetInstructorById(int id) => await _instructorRepository.GetInstructorById(id);

        public async Task CreateInstructor(Instructor instructor) => await _instructorRepository.CreateInstructor(instructor);

        public async Task UpdateInstructor(int id, Instructor instructor) => await _instructorRepository.UpdateInstructor(id, instructor);

        public async Task DeleteInstructor(int id) => await _instructorRepository.DeleteInstructor(id);

        public async Task BulkInsertInstructors(List<Instructor> instructors) => await _instructorRepository.BulkInsertInstructors(instructors);

        public async Task<object> GetInstructorsWithModules() => await _instructorRepository.GetInstructorsWithModules();

        public async Task<int> GetInstructorCount() => await _instructorRepository.GetInstructorCount();

        public async Task<List<int>> GetDistinctHireYears() => await _instructorRepository.GetDistinctHireYears();

        public async Task<object> GetModuleCountPerInstructor() => await _instructorRepository.GetModuleCountPerInstructor();
    }
}
