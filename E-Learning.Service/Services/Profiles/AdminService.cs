using AutoMapper;
using E_learning.Core.Entities.Identity;
using E_Learning.Core.Base;
using E_Learning.Core.Entities.Profiles;
using E_Learning.Core.Interfaces.Repositories;
using E_Learning.Core.Interfaces.Repositories.Academic;
using E_Learning.Core.Interfaces.Repositories.Profile;
using E_Learning.Core.Repository;
using E_Learning.Repository.Repositories.GenericesRepositories.Profile;
using E_Learning.Service.Contract;
using E_Learning.Service.DTOs.Profiles.Admin;
using E_Learning.Service.DTOs.Profiles.Instructor;
using E_Learning.Service.DTOs.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace E_Learning.Service.Services.Profiles
{
    public class AdminProfileService : IAdminProfileService
    {
        private readonly IAdminProfileRepository _adminProfileRepository;
        private readonly IGenericRepository<ApplicationUser, Guid> _genericRepository;
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly ResponseHandler _responseHandler;
        private readonly IFileService _fileService;
        private readonly IInstructorProfileRepository _instructorProfileRepository;
        private readonly IStudentProfileRepository _StudentProfileRepository;
        private readonly ILevelRepository _levelRepository;



        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        public AdminProfileService(
            IAdminProfileRepository adminProfileRepository,
            IGenericRepository<ApplicationUser, Guid> genericRepository,
            IUnitOfWork unit,
            IMapper mapper,
            ResponseHandler responseHandler, IFileService fileService, IInstructorProfileRepository instructorProfileRepository, IStudentProfileRepository studentProfileRepository,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager
            , ILevelRepository levelRepository)
        {
            _adminProfileRepository = adminProfileRepository;
            _genericRepository = genericRepository;
            _unit = unit;
            _mapper = mapper;
            _responseHandler = responseHandler;
            _fileService = fileService;
            _instructorProfileRepository = instructorProfileRepository;
            _StudentProfileRepository = studentProfileRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _levelRepository = levelRepository;
        }

        // ================= Create User Profile =================

        async Task<Response<CreateUserResponseDto>> IAdminProfileService.CreateUserProfile(CreateUserDto dto, CancellationToken ct)
        {
            var user = new ApplicationUser
            {
                FullName = dto.FullName,
                Email = dto.Email,
                UserName = dto.Email,
                PhoneNumber = dto.phoneNumber,
                IsActive = false,
                MemberSince = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                return _responseHandler.BadRequest<CreateUserResponseDto>(errors);
            }

            if (!await _roleManager.RoleExistsAsync(dto.Role))
                await _roleManager.CreateAsync(new IdentityRole<Guid>(dto.Role));

            await _userManager.AddToRoleAsync(user, dto.Role);

            CreateUserResponseDto response;

            if (dto.Role == "Admin")
            {
                var profile = new AdminProfile { AppUserId = user.Id };
                await _adminProfileRepository.AddAsync(profile);
                await _unit.SaveChangesAsync(ct);

                response = _mapper.Map<CreateUserResponseDto>(profile);
            }
            else if (dto.Role == "Instructor")
            {
                var profile = new InstructorProfile { AppUserId = user.Id };
                await _instructorProfileRepository.AddAsync(profile);
                await _unit.SaveChangesAsync(ct);

                response = _mapper.Map<CreateUserResponseDto>(profile);
            }
            else if (dto.Role == "Student")
            {
                // 1. التأكد من تحويل قيمة Level من الـ DTO إلى int (لأنها تأتي string من Swagger)
                int.TryParse(dto.Level, out int levelId);

                var profile = new StudentProfile
                {
                    AppUserId = user.Id,
                    LevelId = levelId > 0 ? levelId : null // إسناد الـ ID للبروفايل
                };

                await _StudentProfileRepository.AddAsync(profile);
                await _unit.SaveChangesAsync(ct);

                // 2. عمل الـ Mapping
                response = _mapper.Map<CreateUserResponseDto>(profile);

                // 3. حل مشكلة الـ N/A: 
                // بما أن الـ profile.Level لسه null (لأنه لم يُسحب من الـ DB بـ Include)
                // نضع القيمة يدوياً في الـ response لكي تظهر للمستخدم فوراً
                if (levelId > 0)
                {
                    // إذا أردتِ جلب الاسم الحقيقي من الـ Repository:
                    var levelData = await _levelRepository.GetByIdAsync(levelId);
                    response.Level = levelData?.Name ?? $"Level {levelId}";
                }
                else
                {
                    response.Level = "N/A";
                }
            }

            else
            {

                return _responseHandler.BadRequest<CreateUserResponseDto>("Invalid role");
            }

            response.Role = dto.Role;

            return _responseHandler.Created(response);
        }



       // ================= Update Admin Profile =================
        public async Task<Response<AdminProfileResponseDto>> UpdateAdminProfile(Guid userId, UpdateAdminProfileDto dto, CancellationToken ct)
        {
            // استدعاء الميثود بمعامل واحد (userId) ليتوافق مع الـ Repository الحالي
            var profile = await _adminProfileRepository.GetAdminProfileWithUserByUserIdAsync(userId);
            
            if (profile == null)
                return _responseHandler.NotFound<AdminProfileResponseDto>("Admin profile not found");

            var user = profile.AppUser;

            // ================= تعديل الرول =================
            var currentRoles = await _userManager.GetRolesAsync(user);
            if (currentRoles.Any())
            {
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
            }

            if (!await _roleManager.RoleExistsAsync(dto.Role))
            {
                await _roleManager.CreateAsync(new IdentityRole<Guid>(dto.Role));
            }
            await _userManager.AddToRoleAsync(user, dto.Role);

            // ================= تعديل الباسورد =================
            if (!string.IsNullOrEmpty(dto.Password))
            {
                if (await _userManager.HasPasswordAsync(user))
                {
                    // إزالة الباسورد القديم
                    await _userManager.RemovePasswordAsync(user);
                }
                // إضافة الباسورد الجديد
                await _userManager.AddPasswordAsync(user, dto.Password);
            }

            // ================= تعديل باقي البيانات =================
            if (!string.IsNullOrEmpty(profile.ProfilePicture))
            {
                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", profile.ProfilePicture.TrimStart('/'));
                if (File.Exists(oldPath))
                    File.Delete(oldPath);
            }

            var newPath = await _fileService.UploadFileAsync<AdminProfile>(dto.ProfilePicture, "images/admins");

            user.FullName = dto.FullName;
            user.Email = dto.Email;
            user.PhoneNumber = dto.PhoneNumber;
            profile.ProfilePicture = newPath;

            // تمرير ct لعملية الحفظ
            await _unit.SaveChangesAsync(ct);

            var resultDto = _mapper.Map<AdminProfileResponseDto>(profile);
            resultDto.Role = dto.Role;
            
            return _responseHandler.Success(resultDto);
        }

        // ================= Get Admin Profile by UserId =================
        public async Task<Response<AdminProfileResponseDto>> GetAdminProfileByUserId(Guid userId, CancellationToken ct)
        {
            // استدعاء الميثود بمعامل واحد فقط
            var profile = await _adminProfileRepository.GetAdminProfileWithUserByUserIdAsync(userId);
            
            if (profile == null)
                return _responseHandler.NotFound<AdminProfileResponseDto>("Admin profile not found");

            var resultDto = _mapper.Map<AdminProfileResponseDto>(profile);
            resultDto.Role = "Admin";

            return _responseHandler.Success(resultDto);
        }

        // ================= Check if Admin Profile Exists =================
        public async Task<Response<bool>> AdminProfileExists(Guid userId)
        {
            var exists = await _adminProfileRepository.GetAdminProfileWithUserByUserIdAsync(userId) != null;
            return _responseHandler.Success(exists);
        }

        // ================= Get All Admins =================
        public async Task<Response<IEnumerable<AdminProfileResponseDto>>> GetAllAdmins()
        {
            var profiles = await _adminProfileRepository.GetAllAdminProfilesWithUsersAsync();
            var resultDtos = _mapper.Map<IEnumerable<AdminProfileResponseDto>>(profiles);
            foreach (var dto in resultDtos)
            {
                dto.Role = "Admin";
            }
            return _responseHandler.Success(resultDtos);
        }

        // ================= Delete Admin Profile =================
        public async Task<Response<AdminProfileResponseDto>> DeleteAdminProfile(Guid userId, CancellationToken ct)
        {
            var profile = await _adminProfileRepository.GetAdminProfileWithUserByUserIdAsync(userId);

            if (profile == null)
                return _responseHandler.NotFound<AdminProfileResponseDto>("Admin profile not found");

            _adminProfileRepository.Remove(profile);
            await _unit.SaveChangesAsync();

            return _responseHandler.Deleted<AdminProfileResponseDto>("Admin profile deleted successfully");
        }

        // 2.2 - (Get) Read Users
        public async Task<Response<IEnumerable<CreateUserResponseDto>>> GetAllUsers(CancellationToken ct)
        {
            var users = await _userManager.Users.ToListAsync(ct);
            var responseList = new List<CreateUserResponseDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var role = roles.FirstOrDefault() ?? "No Role";

                // 1. جلب البروفايل (قد يكون null)
                dynamic? profile = role switch
                {
                    "Student" => await _StudentProfileRepository.GetStudentProfileWithUserByUserIdAsync(user.Id),
                    "Instructor" or "Teacher" => await _instructorProfileRepository.GetInstructorProfileWithUserByUserIdAsync(user.Id),
                    // أضيفي الأدمن هنا إذا كان له مستودع
                    _ => null
                };

                // 2. معالجة البيانات (Mapping)
                CreateUserResponseDto mapped;

                if (profile != null)
                {
                    // إذا وجدنا بروفايل، نحوله هو (لأنه يحتوي على الليفل واليوزر معاً)
                    mapped = _mapper.Map<CreateUserResponseDto>(profile);
                }
                else
                {
                    // إذا لم نجد بروفايل، نحول اليوزر الأساسي فقط
                    mapped = _mapper.Map<CreateUserResponseDto>(user);
                    mapped.Level = "N/A";
                }

                // 3. تأكيد البيانات الإضافية
                mapped.Role = role;
                mapped.UserId = user.Id;
                if (role != "Student") mapped.Level = "N/A";

                responseList.Add(mapped);
            }

            return _responseHandler.Success(responseList.AsEnumerable());
        }

        // 2.5 - (Put/Post) Change Activity (Status)
        public async Task<Response<string>> ChangeUserStatus(Guid userId, bool newStatus)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return _responseHandler.NotFound<string>("User not found");

            user.IsActive = newStatus;
            user.IsActive = (newStatus == true);
            user.UpdatedAt = DateTime.UtcNow;

            await _userManager.UpdateAsync(user);
            return _responseHandler.Success($"User status changed to {newStatus}");
        }

        // 2.6 & 2.7 - Search and Filter
        public async Task<Response<IEnumerable<CreateUserResponseDto>>> SearchAndFilterUsers(string? search, string? role)
        {
            // البداية من كويري المستخدمين
            var query = _userManager.Users.AsQueryable();

            // 1. فلترة بالاسم أو الايميل (اختياري)
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(u => u.FullName.Contains(search) || u.Email.Contains(search));
            }

            var users = await query.ToListAsync();
            var responseList = new List<CreateUserResponseDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userRole = roles.FirstOrDefault();

                // 2. فلترة بالرول (اختياري)
                if (!string.IsNullOrWhiteSpace(role) &&
                    !string.Equals(userRole, role, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                // 3. جلب البروفايل لضمان ظهور الـ Level في نتائج البحث
                dynamic? profile = userRole switch
                {
                    "Student" => await _StudentProfileRepository.GetStudentProfileWithUserByUserIdAsync(user.Id),
                    "Instructor" or "Teacher" => await _instructorProfileRepository.GetInstructorProfileWithUserByUserIdAsync(user.Id),
                    _ => null
                };

                // 4. عمل المابينج بناءً على المتوفر (بروفايل أو يوزر)
                var mapped = (profile != null)
                    ? _mapper.Map<CreateUserResponseDto>(profile)
                    : _mapper.Map<CreateUserResponseDto>(user);

                mapped.Role = userRole ?? "No Role";
                mapped.UserId = user.Id;

                // تنظيف قيمة الـ Level لغير الطلاب
                if (userRole != "Student") mapped.Level = "N/A";

                responseList.Add(mapped);
            }

            return _responseHandler.Success(responseList.AsEnumerable());
        }
        // 2.4 - (Delete) Delete User
        public async Task<Response<string>> DeleteUser(Guid userId,CancellationToken ct)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return _responseHandler.NotFound<string>("User not found");

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded) return _responseHandler.BadRequest<string>("Delete failed");

            return _responseHandler.Deleted<string>("User deleted successfully");
        }

        async public Task<Response<string>> UpdateUser(Guid userId, UpdateUserDto dto, CancellationToken ct)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return _responseHandler.NotFound<string>("User not found");

            user.FullName = dto.FullName;
            user.Email = dto.Email;
            user.UserName = dto.Email;
            user.PhoneNumber = dto.phoneNumber;
            user.UpdatedAt = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return _responseHandler.BadRequest<string>("Update failed");

            return _responseHandler.Success("Updated Successfully");
        }

    }
}