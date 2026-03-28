﻿﻿using SecondSemesterProject.Models;

namespace SecondSemesterProject.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> FetchAllStudentsAsync();

        Task AddStudentAsync(Student student);

        Task<Student?> FetchStudentByIdAsync(int studentId);

        Task UpdateStudentAsync(int id, Student student);

        Task<List<Course>> FetchStudentCoursesAsync(int studentId);

        Task<bool> DeleteStudentAsync(int studentId);

        Task AddStudentsAsync(List<Student> students);

        Task<List<object>> FetchAllStudentsWithCoursesAsync();

        Task<int> FetchStudentsCountAsync();

        Task<List<object>> FetchStudentsFullDetailsAsync();
    }
}
