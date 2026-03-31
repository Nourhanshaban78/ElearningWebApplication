using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using E_Learning.Service.Services.UserDashboard;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserDashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

    public UserDashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

   [HttpGet("summary/{studentId}")] 
public async Task<IActionResult> GetSummary(Guid studentId)
{
    // بننادي السيرفس وبنمرر الـ ID اللي وصل في الـ URL
    var data = await _dashboardService.GetStudentDashboardDataAsync(studentId);
    
    if (data == null)
    {
        return NotFound($"No dashboard data found for student with ID: {studentId}");
    }

    return Ok(data);
}
    }
}