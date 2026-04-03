using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using E_Learning.Core.Base;
using E_Learning.Core.Entities.LiveSessions;
using E_Learning.Core.Enums;
using E_Learning.Core.Repository;
using E_Learning.Service.DTOs.Enrollments.Enrollment;
using E_Learning.Service.DTOs.LiveSessionDto;
using E_Learning.Service.Hubs;
using E_Learning.Service.Services.Profiles;
using E_Learning.Service.Services.Profiles.StudentSetting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Service.Services.LiveSessionServices
{
    public class LiveSessionAttendeeService : ILiveSessionAttendeeService
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;
        private readonly IMapper _mapper;
        private readonly IHubContext<LiveSessionHub> _hubContext;
        private readonly IStudentService _studentService;

        public LiveSessionAttendeeService(
            IUnitOfWork uow,
            ResponseHandler responseHandler,
            IMapper mapper,
            IHubContext<LiveSessionHub> hubContext,
            IStudentService studentService)
        {
            _uow = uow;
            _responseHandler = responseHandler;
            _mapper = mapper;
            _hubContext = hubContext;
            _studentService = studentService;
        }

        public async Task<Response<AttendeeResponseDto>> LogAttendanceAsync(LogAttendanceDto dto, CancellationToken ct = default)
        {
            var session = await _uow.LiveSessions.GetByIdAsync(dto.SessionId, ct);
            if (session == null) return _responseHandler.NotFound<AttendeeResponseDto>("Session not found.");

            var attendees = await _uow.LiveSessionAttendees.GetAttendeesBySessionIdAsync(dto.SessionId, ct);
            var existingRecord = attendees.FirstOrDefault(x => x.StudentId == dto.StudentId);

            if (existingRecord != null)
            {
                if (existingRecord.LeftAt == null)
                    return _responseHandler.BadRequest<AttendeeResponseDto>("Student is already active in this session.");

                existingRecord.JoinedAt = DateTime.UtcNow;
                existingRecord.LeftAt = null;
                existingRecord.DurationSeconds = null;

                _uow.LiveSessionAttendees.Update(existingRecord);
            }
            else
            {
                var attendee = _mapper.Map<LiveSessionAttendee>(dto);
                attendee.JoinedAt = DateTime.UtcNow;
                await _uow.LiveSessionAttendees.AddAsync(attendee, ct);
            }

            await _uow.SaveChangesAsync(ct);

            var fullData = await _uow.LiveSessionAttendees.GetFullActiveAttendeeAsync(dto.SessionId, dto.StudentId, ct);
            var result = _mapper.Map<AttendeeResponseDto>(fullData);

            // 🟢 تعديل: تغيير اسم الميثود إلى GetStudentProfileByUserIdAsync لتطابق الـ Interface
            var studentProfile = await _studentService.GetStudentProfileByUserIdAsync(dto.StudentId);
            if (studentProfile.Data != null)
            {
                result.Student.ProfilePicture = studentProfile.Data.ProfilePicture;
                result.Student.Location = studentProfile.Data.Location;
            }

            await _hubContext.Clients.Group(dto.SessionId.ToString()).SendAsync("OnStudentJoined", result);
            return _responseHandler.Success(result);
        }

        public async Task<Response<AttendeeResponseDto>> LeaveSession(LeaveSessionDto dto, CancellationToken ct = default)
        {
            var existing = await _uow.LiveSessionAttendees.GetFullActiveAttendeeAsync(dto.SessionId, dto.StudentId, ct);

            if (existing == null)
                return _responseHandler.BadRequest<AttendeeResponseDto>("Student is not in the session or already left.");

            _uow.LiveSessionAttendees.LeaveSession(existing);
            await _uow.SaveChangesAsync(ct);

            var result = _mapper.Map<AttendeeResponseDto>(existing);

            // 🟢 تعديل: تغيير اسم الميثود إلى GetStudentProfileByUserIdAsync
            var studentProfileResponse = await _studentService.GetStudentProfileByUserIdAsync(dto.StudentId);
            if (studentProfileResponse.Data != null)
            {
                result.Student.ProfilePicture = studentProfileResponse.Data.ProfilePicture;
                result.Student.Location = studentProfileResponse.Data.Location;
            }

            await _hubContext.Clients.Group(dto.SessionId.ToString()).SendAsync("OnStudentLeft", result);

            return _responseHandler.Success(result);
        }

        public async Task<Response<SessionAttendeesDashboardDto>> GetAttendeesBySessionIdAsync(int sessionId, CancellationToken ct = default)
        {
            var attendees = await _uow.LiveSessionAttendees.GetAttendeesBySessionIdAsync(sessionId, ct);

            if (attendees == null || !attendees.Any())
            {
                var session = await _uow.LiveSessions.GetByIdAsync(sessionId, ct);
                return _responseHandler.Success(new SessionAttendeesDashboardDto
                {
                    LiveSession = _mapper.Map<LiveSessionResponseDto>(session),
                    Attendees = new List<AttendeeSummaryDto>()
                });
            }

            var firstRecord = attendees.First();
            var sessionDto = _mapper.Map<LiveSessionResponseDto>(firstRecord.Session);

            var attendeesList = attendees.Select(a => new AttendeeSummaryDto
            {
                StudentId = a.StudentId,
                Student = _mapper.Map<StudentSummaryDto>(a.Student),
                JoinedAt = a.JoinedAt,
                LeftAt = a.LeftAt,
                DurationSeconds = a.DurationSeconds
            }).ToList();

            // 🟢 تعديل: تغيير اسم الميثود إلى GetAllStudentsAsync لتطابق الـ Interface
            var allStudentsResponse = await _studentService.GetAllStudentsAsync();
            var allStudents = allStudentsResponse.Data;

            foreach (var item in attendeesList)
            {
                // تعديل: البحث عن الطالب بناءً على الـ UserId المرتبط بالبروفايل
                var extra = allStudents?.FirstOrDefault(s => s.Id.ToString() == item.StudentId.ToString());
                if (extra != null)
                {
                    item.Student.ProfilePicture = extra.ProfilePicture;
                    item.Student.Location = extra.Location;
                }
            }

            var result = new SessionAttendeesDashboardDto
            {
                LiveSession = sessionDto,
                Attendees = attendeesList
            };

            return _responseHandler.Success(result);
        }
    }
}