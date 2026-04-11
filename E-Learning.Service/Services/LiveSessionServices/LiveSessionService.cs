using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using E_Learning.Core.Base;
using E_Learning.Core.Entities.LiveSessions;
using E_Learning.Core.Enums;
using E_Learning.Core.Interfaces.Services.Courses;
using E_Learning.Core.Repository;
using E_Learning.Service.DTOs.LiveSessionDto;
using E_Learning.Service.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using E_Learning.Service.Services.Profiles.InstructorSetting;

namespace E_Learning.Service.Services.LiveSessionServices
{
    public class LiveSessionService : ILiveSessionService
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;
        private readonly IMapper _mapper;
        private readonly ICourseService _courseService;
        private readonly IHubContext<LiveSessionHub> _hubContext;
        private readonly IHttpClientFactory _httpClientFactory;

        public LiveSessionService(
            IUnitOfWork uow,
            ResponseHandler responseHandler,
            IMapper mapper,
            ICourseService courseService,
            IHubContext<LiveSessionHub> hubContext,
            IHttpClientFactory httpClientFactory)
        {
            _uow = uow;
            _responseHandler = responseHandler;
            _mapper = mapper;
            _courseService = courseService;
            _hubContext = hubContext;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Response<IReadOnlyList<LiveSessionResponseDto>>> GetAllAsync(string? search, string? status, CancellationToken ct = default)
        {
            // 1. نبدأ الاستعلام مع كل الـ Includes اللي بيحتاجها الـ Mapping Profile
            var query = _uow.LiveSessions.QueryNoTracking()
                            .Include(x => x.Course)
                            .Include(x => x.Instructor)
                            .Include(x => x.Attendees)
                                .ThenInclude(a => a.Student)
                            .AsQueryable();

            // 2. الفلترة (إذا موجودة)
            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(x => x.Title.Contains(search));

            if (!string.IsNullOrWhiteSpace(status) && Enum.TryParse<LiveSessionStatus>(status, true, out var statusEnum))
                query = query.Where(x => x.Status == statusEnum);

            // 3. التنفيذ وجلب البيانات
            var sessions = await query.ToListAsync(ct);

            if (sessions == null || !sessions.Any())
                return _responseHandler.NotFound<IReadOnlyList<LiveSessionResponseDto>>("No live sessions found.");

            // 4. التحويل (هنا المابر رح يلاقي Course و Instructor جاهزين ويعبي الـ DTO كامل)
            var mappedData = _mapper.Map<List<LiveSessionResponseDto>>(sessions);

            return _responseHandler.Success((IReadOnlyList<LiveSessionResponseDto>)mappedData);
        }
        public async Task<Response<LiveSessionResponseDto>> GetByIdAsync(int id, CancellationToken ct = default)
        {
            // الـ Repository الجديد صار يعمل Include تلقائياً بفضل الـ override اللي عملناه
            var session = await _uow.LiveSessions.GetByIdAsync(id, ct);

            if (session is null)
                return _responseHandler.NotFound<LiveSessionResponseDto>($"Live Session with ID {id} not found.");

            var dto = _mapper.Map<LiveSessionResponseDto>(session);
            return _responseHandler.Success(dto);
        }

        public async Task<Response<LiveSessionResponseDto>> CreateAsync(CreateLiveSessionDto dto, CancellationToken ct)
        {
            var courseResponse = await _courseService.GetCourseByIdAsync(dto.CourseId, ct);
            if (courseResponse.Data == null)
                return _responseHandler.NotFound<LiveSessionResponseDto>("Course not found.");

            var session = _mapper.Map<LiveSession>(dto);

            // استخدام ميثود الـ Generic AddAsync
            await _uow.LiveSessions.AddAsync(session, ct);
            await _uow.SaveChangesAsync(ct);

            string zoomJoinUrl = string.Empty;

            try
            {
                var (startUrl, joinUrl) = await CreateZoomMeetingAsync(session, ct);
                zoomJoinUrl = joinUrl;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Zoom Error: {ex.Message}");
            }

            var result = await GetByIdAsync(session.Id, ct);

            if (result.Data != null)
            {
                result.Data.ZoomUrl = zoomJoinUrl;
            }

            await _hubContext.Clients.All.SendAsync("OnSessionCreated", result.Data);
            return _responseHandler.Created(result.Data);
        }

        public async Task<Response<LiveSessionResponseDto>> UpdateAsync(int id, UpdateLiveSessionDto dto, CancellationToken ct)
        {
            var session = await _uow.LiveSessions.GetByIdAsync(id, ct);
            if (session is null)
                return _responseHandler.NotFound<LiveSessionResponseDto>($"Live Session {id} not found.");

            var courseResponse = await _courseService.GetCourseByIdAsync(dto.CourseId, ct);
            if (courseResponse.Data == null)
                return _responseHandler.NotFound<LiveSessionResponseDto>("Course not found.");

            _mapper.Map(dto, session);

            // استخدام ميثود الـ Generic Update
            _uow.LiveSessions.Update(session);
            await _uow.SaveChangesAsync(ct);

            var result = await GetByIdAsync(id, ct);
            await _hubContext.Clients.All.SendAsync("OnSessionUpdated", result.Data);
            return _responseHandler.Success(result.Data);
        }

        public async Task<Response<LiveSessionResponseDto>> DeleteAsync(int id, CancellationToken ct)
        {
            var result = await GetByIdAsync(id, ct);
            if (result.Data == null)
                return _responseHandler.NotFound<LiveSessionResponseDto>("Session not found.");

            var entityToDelete = await _uow.LiveSessions.GetByIdAsync(id, ct);

            // استخدام الـ SoftDelete الخاصة بالـ Repo
            _uow.LiveSessions.SoftDelete(entityToDelete);
            await _uow.SaveChangesAsync(ct);

            await _hubContext.Clients.All.SendAsync("OnSessionDeleted", id);
            return _responseHandler.Success(result.Data);
        }

        // --- Zoom Methods (بقيت كما هي لأنها منطق خارجي) ---
        private async Task<string> GetZoomAccessTokenAsync(HttpClient client, CancellationToken ct)
        {
            string clientId = "JRcfzlFpQDmUPPYjamyefQ";
            string clientSecret = "gUz6T6b2ph5uNLxbpwV6096Y1jfpJhhw";
            string accountId = "v5qj43RhQ3uX0yYvFVqyBA";

            var authHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
            var request = new HttpRequestMessage(HttpMethod.Post, $"https://zoom.us/oauth/token?grant_type=account_credentials&account_id={accountId}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", authHeader);

            var response = await client.SendAsync(request, ct);
            var responseBody = await response.Content.ReadAsStringAsync(ct);

            if (!response.IsSuccessStatusCode) throw new Exception($"Zoom Auth Failed: {responseBody}");

            var data = System.Text.Json.JsonDocument.Parse(responseBody).RootElement;
            return data.TryGetProperty("access_token", out var tokenElement) ? tokenElement.GetString() ?? "" : throw new Exception("Token not found.");
        }

        public async Task<(string startUrl, string joinUrl)> CreateZoomMeetingAsync(LiveSession session, CancellationToken ct)
        {
            var client = _httpClientFactory.CreateClient();
            var accessToken = await GetZoomAccessTokenAsync(client, ct);

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.zoom.us/v2/users/me/meetings");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            request.Content = JsonContent.Create(new
            {
                topic = session.Title,
                type = 2,
                start_time = session.ScheduledAt.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                duration = session.DurationMinutes > 0 ? session.DurationMinutes : 60,
                settings = new { join_before_host = false, waiting_room = true }
            });

            var response = await client.SendAsync(request, ct);
            var responseBody = await response.Content.ReadAsStringAsync(ct);

            if (!response.IsSuccessStatusCode) return ("", "");

            var data = System.Text.Json.JsonDocument.Parse(responseBody).RootElement;
            return (data.GetProperty("start_url").GetString() ?? "", data.GetProperty("join_url").GetString() ?? "");
        }
    }
}