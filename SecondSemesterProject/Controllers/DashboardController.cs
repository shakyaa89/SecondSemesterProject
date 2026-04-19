using Microsoft.AspNetCore.Mvc;
using SecondSemesterProject.Application.Interfaces.IService;

namespace SecondSemesterProject.Controllers
{
    [ApiController]
    [Route("api")]
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("dashboard/summary")]
        public async Task<IActionResult> GetSummary()
        {
            var summary = await _dashboardService.GetSummary();
            return Ok(summary);
        }
    }
}
