

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SecondSemesterProject.Application.DTO;
using SecondSemesterProject.Application.Interfaces.IService;
using SecondSemesterProject.Domain.Models;

namespace SecondSemesterProject.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register-student")]
        public async Task<IActionResult> RegisterStudent(RegisterDTO dto)
        {
            var user = new Users
            {
                UserName = dto.UserName,
                Email = dto.Email
            };

            var result = await _authService.RegisterStudent(user, dto.Password);
            return Ok(new { message = result });
        }

        [HttpPost("register-instructor")]
        public async Task<IActionResult> RegisterInstructor(RegisterDTO dto)
        {
            var user = new Users
            {
                UserName = dto.UserName,
                Email = dto.Email
            };

            var result = await _authService.RegisterInstructor(user, dto.Password);
            return Ok(new { message = result });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var result = await _authService.Login(dto.UserName, dto.Password);
            return Ok(new { message = result });
        }
    }
}
