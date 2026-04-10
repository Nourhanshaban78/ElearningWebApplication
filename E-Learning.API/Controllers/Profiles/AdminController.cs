using E_Learning.Core.Base;
using E_Learning.Service.DTOs.Profiles.Admin;
using E_Learning.Service.DTOs.Profiles.Instructor;
using E_Learning.Service.DTOs.Profiles.Student;
using E_Learning.Service.DTOs.User;
using E_Learning.Service.Services.Profiles;
using E_Learning.Service.Services.Profiles.InstructorSetting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly IAdminProfileService _adminService;
    private readonly IInstructorService _instructorService;

    public AdminController(IAdminProfileService adminService, IInstructorService instructorService)
    {
        _adminService = adminService;
        _instructorService = instructorService;
    }

    // ================= Create User =================
    [HttpPost("createUser")]
    public async Task<IActionResult> CreateUserProfile([FromForm] CreateUserDto dto)
    {
        var response = await _adminService.CreateUserProfile(dto);
        return StatusCode((int)response.HttpStatusCode, response);
    }

    // ================= 2.2 - Get All Users =================
    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers(CancellationToken ct)
    {
        var response = await _adminService.GetAllUsers(ct);
        return StatusCode((int)response.HttpStatusCode, response);
    }

    // ================= 2.3 - Update User =================
    [HttpPut("users/{userId}")]
    public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] UpdateUserDto dto,CancellationToken ct)
    {
        var response = await _adminService.UpdateUser(userId, dto,ct);
        return StatusCode((int)response.HttpStatusCode, response);
    }

    // ================= 2.4 - Delete User =================
    [HttpDelete("users/{userId}")]
    public async Task<IActionResult> DeleteUser(Guid userId,CancellationToken ct)
    {
        var response = await _adminService.DeleteUser(userId,ct);
        return StatusCode((int)response.HttpStatusCode, response);
    }

    // ================= 2.5 - Change User Status (Activity) =================
    [HttpPut("users/{userId}/status")]
    public async Task<IActionResult> ChangeUserStatus(Guid userId, [FromQuery] bool newStatus)
    {
        var response = await _adminService.ChangeUserStatus(userId, newStatus);
        return StatusCode((int)response.HttpStatusCode, response);
    }

    // ================= 2.6 & 2.7 - Search and Filter =================
    [HttpGet("users/search")]
    public async Task<IActionResult> SearchAndFilterUsers([FromQuery] string? search, [FromQuery] string? role)
    {
        var response = await _adminService.SearchAndFilterUsers(search, role);
        return StatusCode((int)response.HttpStatusCode, response);
    }

    // ================= Admin Specific Operations =================

    [HttpPut("profile/{userId}")]
    public async Task<IActionResult> UpdateAdminProfile(Guid userId, [FromForm] UpdateAdminProfileDto dto,CancellationToken ct)
    {
        var response = await _adminService.UpdateAdminProfile(userId, dto,ct);
        return StatusCode((int)response.HttpStatusCode, response);
    }

    [HttpGet("profile/{userId}")]
    public async Task<IActionResult> GetAdminByUserId(Guid userId,CancellationToken ct)
    {
        var response = await _adminService.GetAdminProfileByUserId(userId,ct);
        return StatusCode((int)response.HttpStatusCode, response);
    }

    [HttpGet("all-admins")]
    public async Task<IActionResult> GetAllAdmins()
    {
        var response = await _adminService.GetAllAdmins();
        return StatusCode((int)response.HttpStatusCode, response);
    }
}