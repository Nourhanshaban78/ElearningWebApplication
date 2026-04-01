using E_Learning.Core.Base;
using E_Learning.Core.Repository;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Core.Features.Quizzes.Queries.GetQuizAttempts
{
    public class GetQuizAttemptsHandler : IRequestHandler<GetQuizAttemptsQuery, Response<List<AttemptSummaryDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ResponseHandler _responseHandler;

        public GetQuizAttemptsHandler(
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor,
            ResponseHandler responseHandler)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _responseHandler = responseHandler;
        }

        public async Task<Response<List<AttemptSummaryDto>>> Handle(GetQuizAttemptsQuery request, CancellationToken ct)
        {
            // 1) تأكد إن الكويز موجود
            var quiz = await _unitOfWork.Quizzes.GetWithCourseAsync(request.QuizId, ct);
            if (quiz == null)
                return _responseHandler.NotFound<List<AttemptSummaryDto>>("Quiz not found");

            // بعد ما تجيبي الكويز
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var instructorId))
                return _responseHandler.Unauthorized<List<AttemptSummaryDto>>();

            var isAdmin = _httpContextAccessor.HttpContext?.User?.IsInRole("Admin") ?? false;

            // تأكد إن الكويز بتاع الـ Instructor ده
            if (!isAdmin && quiz.Course.InstructorId != instructorId)
                return _responseHandler.Forbidden<List<AttemptSummaryDto>>("This quiz is not in your course");

            // 2) جيب كل الـ Attempts
            var attempts = await _unitOfWork.QuizAttempts.GetByQuizIdAsync(request.QuizId, ct);

            // 3) Map Response
            var result = attempts.Select(a => new AttemptSummaryDto
            {
                AttemptId = a.Id,
                StudentId = a.StudentId,
                StudentName = a.Student?.FullName ?? "",
                Score = a.Score,
                IsPassed = a.IsPassed,
                Status = a.Status.ToString(),
                StartedAt = a.StartedAt,
                SubmittedAt = a.SubmittedAt
            }).ToList();

            return _responseHandler.Success(result);
        }
    }
}
