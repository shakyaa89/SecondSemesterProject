using SecondSemesterProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecondSemesterProject.Application.Interfaces.IService
{
    public interface IAuthService
    {
        Task<string> Login(string username, string password);
        Task<string> RegisterStudent(Users user, string password);
        Task<string> RegisterInstructor(Users user, string password);
    }
}
