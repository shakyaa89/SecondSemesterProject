using Microsoft.AspNetCore.Identity;
using SecondSemesterProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecondSemesterProject.Application.Interfaces.IService
{
    public interface IJwtTokenService
    {
        Task<String> GenerateUserToken(Users user);
    }
}
