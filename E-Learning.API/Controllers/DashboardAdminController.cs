using E_Learning.Service.Services.AdminDashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardAdminController : ControllerBase
    {
        private readonly IAdminDashboardService _dashboardService;

        public DashboardAdminController(IAdminDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("overview")]
        public async Task<IActionResult> GetOverview(CancellationToken ct)
        {
            var response = await _dashboardService.GetDashboardOverviewAsync(ct);
 
            if (response.Succeeded)
                return Ok(response);

            return BadRequest(response);
        }
    }
}
