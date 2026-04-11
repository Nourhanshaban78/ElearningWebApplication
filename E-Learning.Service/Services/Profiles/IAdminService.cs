using E_Learning.Core.Base;
using E_Learning.Core.Entities.Profiles;
using E_Learning.Service.DTOs.User;
using E_Learning.Service.DTOs.Profiles.Instructor;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using E_Learning.Service.DTOs.Profiles.Admin;

namespace E_Learning.Service.Services.Profiles
{
    public interface IAdminProfileService
    {
        Task<Response<AdminProfileResponseDto>> GetAdminProfileByUserId(Guid userId, CancellationToken ct);
        Task<Response<CreateUserResponseDto>> CreateUserProfile(CreateUserDto dto, CancellationToken ct = default);
        // Task<Response<InstructorProfileResponseDto>> CreateInstructorProfile(CreateInstructorProfileDto dto, CancellationToken ct = default);
        Task<Response<AdminProfileResponseDto>> UpdateAdminProfile(Guid userId, UpdateAdminProfileDto dto, CancellationToken ct = default);

        Task<Response<IEnumerable<CreateUserResponseDto>>> GetAllUsers(CancellationToken ct);
        Task<Response<string>> UpdateUser(Guid userId, CreateUserDto dto, CancellationToken ct);
        Task<Response<string>> ChangeUserStatus(Guid userId, bool newStatus);
        Task<Response<IEnumerable<CreateUserResponseDto>>> SearchAndFilterUsers(string? search, string? role);
        Task<Response<string>> DeleteUser(Guid userId, CancellationToken ct);

        Task<Response<bool>> AdminProfileExists(Guid userId);
        Task<Response<IEnumerable<AdminProfileResponseDto>>> GetAllAdmins();
        Task<Response<AdminProfileResponseDto>> DeleteAdminProfile(Guid userId, CancellationToken ct);

    }
}