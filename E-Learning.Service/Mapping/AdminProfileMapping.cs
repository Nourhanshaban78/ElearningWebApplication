using AutoMapper;
using E_learning.Core.Entities.Identity;
using E_Learning.Core.Entities.Profiles;
using E_Learning.Service.DTOs.Profiles.Admin;
using E_Learning.Service.DTOs.User;

namespace E_Learning.Service.Mapping
{
    public class AdminProfileMapping : Profile
    {
        public AdminProfileMapping()
        {
            CreateMap<ApplicationUser, CreateUserResponseDto>()
        .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
        .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
        .ForMember(dest => dest.Level, opt => opt.Ignore()); // لأن اليوزر العادي ليس له ليفل
        
            // 1. Mapping من AdminProfile إلى CreateUserResponseDto
            CreateMap<AdminProfile, CreateUserResponseDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.AppUserId))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.AppUser.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppUser.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.AppUser.PhoneNumber))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.AppUser.IsActive))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.AppUser.MemberSince))
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src => "N/A"));

            // 2. Mapping من InstructorProfile إلى CreateUserResponseDto
            CreateMap<InstructorProfile, CreateUserResponseDto>()
                 .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.AppUserId))
                 .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.AppUser.FullName))
                 .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppUser.Email))
                 .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.AppUser.PhoneNumber))
                 .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.AppUser.IsActive))
                 .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.AppUser.MemberSince))
                 .ForMember(dest => dest.Level, opt => opt.MapFrom(src => "N/A"));

            // 3. Mapping من StudentProfile إلى CreateUserResponseDto
            CreateMap<StudentProfile, CreateUserResponseDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.AppUserId))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.AppUser.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppUser.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.AppUser.PhoneNumber))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.AppUser.IsActive))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.AppUser.MemberSince))
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level != null ? src.Level.Name : "N/A"));

                 CreateMap<CreateAdminProfileDto, AdminProfile>();

            CreateMap<AdminProfile, AdminProfileResponseDto>()

                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.AppUser.FullName))

                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppUser.Email))

                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.AppUser.PhoneNumber))

                .ForMember(dest => dest.Role, opt => opt.Ignore());
        }
    }
}