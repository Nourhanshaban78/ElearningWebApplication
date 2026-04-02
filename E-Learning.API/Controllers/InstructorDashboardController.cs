using E_Learning.Service.Services.Dashboard.InstructorDashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Learning.API.Controllers
{
    [Authorize(Roles = "Instructor")]
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorDashboardController : ControllerBase
    {
        private readonly IInstructorDashboardService _dashboardService;

        public InstructorDashboardController(IInstructorDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboard(CancellationToken ct = default)
        {
            var result = await _dashboardService.GetDashboardAsync(ct);
            return (int)result.HttpStatusCode switch
            {
                200 => Ok(result),
                404 => NotFound(result),
                _ => StatusCode((int)result.HttpStatusCode, result)
            };
        }


    }
}
