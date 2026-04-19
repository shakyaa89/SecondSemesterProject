using Microsoft.AspNetCore.Identity;
using SecondSemesterProject.Domain.Models;
using SecondSemesterProject.Application.Interfaces.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecondSemesterProject.Infrastructure.Services
{
    public class AuthService: IAuthService
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly RoleManager<Roles> _roleManager;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthService(
            UserManager<Users> userManager,
            SignInManager<Users> signInManager,
            RoleManager<Roles> roleManager, IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<string> Login(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                throw new Exception("Invalid username or password");

            var result = await _signInManager.PasswordSignInAsync(username, password, false, true);

            if (!result.Succeeded)
                throw new Exception("Invalid login attempt");

            return await _jwtTokenService.GenerateUserToken(user);
        }

        public async Task<string> RegisterStudent(Users user, string password)
        {
            return await RegisterUser(user, password, "Student");
        }

        public async Task<string> RegisterInstructor(Users user, string password)
        {
            return await RegisterUser(user, password, "Instructor");
        }

        private async Task<string> RegisterUser(Users user, string password, string role)
        {
            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new Roles { Name = role });
            }

            await _userManager.AddToRoleAsync(user, role);

            return $"{role} registered successfully";
        }
    }
}
