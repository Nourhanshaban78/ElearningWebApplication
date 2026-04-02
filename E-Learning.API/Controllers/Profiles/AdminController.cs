using E_Learning.API.Extensions.E_Learning.API.Extensions;
using E_Learning.Service.DTOs.Profiles;
using E_Learning.Service.DTOs.Profiles.Admin;
using E_Learning.Service.Services.Profiles.AdminSetting;
using E_Learning.Service.Services.Profiles.GenericProfileSettingServices;
using Microsoft.AspNetCore.Authorization;
using E_Learning.Service.DTOs.Profiles.Instructor;
using E_Learning.Service.DTOs.Profiles.Student;
using E_Learning.Service.DTOs.User;
using E_Learning.Service.Services.Profiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
[Route("api/[controller]")]

[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
[Authorize(Roles ="Admin")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;
    private readonly IInstructorService _instructorService;
    private readonly IGenericProfileSettingServices _genericProfileSetting;


    public AdminController(IAdminService adminService, IGenericProfileSettingServices genericProfileSetting)
    {
        _adminService = adminService;
        _genericProfileSetting = genericProfileSetting;
    }

    // ================= Create User =================
    [HttpPost("createUser")]
    public async Task<IActionResult> CreateUserProfile([FromForm] CreateUserDto dto)
    {
        var response = await _adminService.CreateUserProfile(dto);
        return StatusCode((int)response.HttpStatusCode, response);
    }
    /*private Guid UserId =>
        Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);*/

    // ================= 2.2 - Get All Users =================
    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers(CancellationToken ct)

    [HttpGet("settings/notifications")]
    public async Task<IActionResult> GetAdminNotificationSettings(CancellationToken ct)
    {
        var response = await _adminService.GetAllUsers(ct);
        return StatusCode((int)response.HttpStatusCode, response);
    }

    // ================= 2.3 - Update User =================
    [HttpPut("users/{userId}")]
    public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] CreateUserDto dto)
    {
        var response = await _adminService.UpdateUser(userId, dto);
        return StatusCode((int)response.HttpStatusCode, response);
    }
         
        var UserId = User.GetUserId(); //this extention method get id from jwt token and return it as guid

    // ================= 2.4 - Delete User =================
    [HttpDelete("users/{userId}")]
    public async Task<IActionResult> DeleteUser(Guid userId)

        var result = await _adminService.GetAdminNotificationSettingsAsync(UserId, ct);
        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpGet("profile")]
    public async Task<IActionResult> GetAdminProfile(CancellationToken ct)
    {
        var response = await _adminService.DeleteUser(userId);
        return StatusCode((int)response.HttpStatusCode, response);
       var UserId = User.GetUserId();

        var result = await _adminService.GetAdminProfileByUserId(UserId, ct);
        return StatusCode((int)result.HttpStatusCode, result);
    }

    // ================= 2.5 - Change User Status (Activity) =================
    [HttpPut("users/{userId}/status")]
    public async Task<IActionResult> ChangeUserStatus(Guid userId, [FromQuery] bool newStatus)
    [HttpPut("profile")]
    public async Task<IActionResult> UpdateAdminProfile([FromForm] UpdateAdminProfileDto dto, CancellationToken ct)
    {
        var response = await _adminService.ChangeUserStatus(userId, newStatus);
        return StatusCode((int)response.HttpStatusCode, response);
        var UserId = User.GetUserId();
        var result = await _adminService.UpdateAdminProfile(UserId, dto, ct);
        return StatusCode((int)result.HttpStatusCode, result);
    }

    // ================= 2.6 & 2.7 - Search and Filter =================
    [HttpGet("users/search")]
    public async Task<IActionResult> SearchAndFilterUsers([FromQuery] string? search, [FromQuery] string? role)
    [HttpPut("settings/notifications")]
    public async Task<IActionResult> UpdateAdminNotificationSetting([FromBody] AdminNotificationSettingDto dto, CancellationToken ct)
    {
        var response = await _adminService.SearchAndFilterUsers(search, role);
        return StatusCode((int)response.HttpStatusCode, response);
        var UserId = User.GetUserId();
        var result = await _adminService.UpdateAdminNotificationSettingAsync(UserId, dto, ct);
        return StatusCode((int)result.HttpStatusCode, result);
    }

    // ================= Admin Specific Operations =================

    [HttpPut("profile/{userId}")]
    public async Task<IActionResult> UpdateAdminProfile(Guid userId, [FromForm] UpdateAdminProfileDto dto)
    [HttpPut("settings/notifications/preferences")]
    public async Task<IActionResult> UpdateAdminNotificationPreferences([FromBody] AdminNotificationPrefrancesDto dto, CancellationToken ct)
    {
        var response = await _adminService.UpdateAdminProfile(userId, dto);
        return StatusCode((int)response.HttpStatusCode, response);
        var UserId = User.GetUserId();
        var result = await _adminService.UpdateAdminNotification_PrefrancesSettingAsync(UserId, dto, ct);
        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpGet("profile/{userId}")]
    public async Task<IActionResult> GetAdminByUserId(Guid userId)
    [HttpPut("settings/general")]
    public async Task<IActionResult> UpdateGeneralSetting([FromBody] GeneralSettingDto dto, CancellationToken ct)
    {
        var response = await _adminService.GetAdminProfileByUserId(userId);
        return StatusCode((int)response.HttpStatusCode, response);
        var UserId = User.GetUserId();
        var result = await _adminService.UpdateGeneralSettingAsync(UserId, dto, ct);
        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpGet("all-admins")]
    public async Task<IActionResult> GetAllAdmins()
    {
        var response = await _adminService.GetAllAdmins();
        return StatusCode((int)response.HttpStatusCode, response);
    }
    [HttpPut("settings/academic")]
    public async Task<IActionResult> UpdateAcademicSetting([FromBody] AcademicSettingDto dto, CancellationToken ct)
    {
        var UserId = User.GetUserId();
        var result = await _adminService.UpdateAcademicSettingAsync(UserId, dto, ct);
        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpPut("password")]
    public async Task<IActionResult> UpdatePassword(
         [FromBody] ChangePasswordDto dto)
    {
        var result = await _genericProfileSetting.UpdatePasswordAsync(CurrentUserId, dto);
        return StatusCode((int)result.HttpStatusCode, result);
    }
}