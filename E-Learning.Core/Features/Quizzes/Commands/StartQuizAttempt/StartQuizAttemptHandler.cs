using E_Learning.Core.Base;
using E_Learning.Core.Entities.Assessments.Quiz;
using E_Learning.Core.Enums;
using E_Learning.Core.Interfaces.Repositories;
using E_Learning.Core.Repository;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace E_Learning.Core.Features.Quizzes.Commands.StartQuizAttempt
{
    public class StartQuizAttemptHandler : IRequestHandler<StartQuizAttemptCommand, Response<StartQuizAttemptResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ResponseHandler _responseHandler;

        public StartQuizAttemptHandler(
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor,
            ResponseHandler responseHandler)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _responseHandler = responseHandler;
        }

        public async Task<Response<StartQuizAttemptResponse>> Handle(StartQuizAttemptCommand request, CancellationToken ct)
        {
            try
            {
                // 1️⃣ Get StudentId
                var user = _httpContextAccessor.HttpContext?.User;
                if (user == null)
                    return _responseHandler.Unauthorized<StartQuizAttemptResponse>();

                

                var studentIdClaim =
                    user.FindFirst(ClaimTypes.NameIdentifier) ??
                    user.FindFirst("sub") ??
                    user.FindFirst("userId");
                if (studentIdClaim == null || !Guid.TryParse(studentIdClaim.Value, out Guid studentId))
                    return _responseHandler.Unauthorized<StartQuizAttemptResponse>();


                // 2️⃣ Get Quiz
                var quiz = await _unitOfWork.Quizzes.GetByIdAsync(request.QuizId);
                if (quiz == null)
                    return _responseHandler.NotFound<StartQuizAttemptResponse>("Quiz not found");

                if (!quiz.IsActive)
                    return _responseHandler.Forbidden<StartQuizAttemptResponse>("Quiz not active");


                // 3️⃣ Check ScheduledAt (start time)
                if (quiz.ScheduledAt.HasValue && quiz.ScheduledAt > DateTime.UtcNow)
                    return _responseHandler.Forbidden<StartQuizAttemptResponse>(
                        $"Quiz starts at {quiz.ScheduledAt.Value.ToLocalTime()}");


                // 4️⃣ Check EndAt (NEW 🔥)
                if (quiz.EndAt.HasValue && quiz.EndAt <= DateTime.UtcNow)
                    return _responseHandler.Forbidden<StartQuizAttemptResponse>("Quiz has ended");


                // 5️⃣ Check Enrollment
                var enrolled = await _unitOfWork.Enrollments
                    .AnyAsync(e => e.StudentId == studentId && e.CourseId == quiz.CourseId && !e.IsDeleted, ct);

                if (!enrolled)
                    return _responseHandler.Forbidden<StartQuizAttemptResponse>("Not enrolled in this course");


                // 6️⃣ Check MaxAttempts
                var attemptsCount = await _unitOfWork.QuizAttempts
                    .GetAttemptsCountAsync(studentId, request.QuizId, ct);

                if (quiz.MaxAttempts.HasValue && attemptsCount >= quiz.MaxAttempts.Value)
                    return _responseHandler.BadRequest<StartQuizAttemptResponse>("Max attempts reached");


                // 7️⃣ Check Active Attempt
                var activeAttempt = await _unitOfWork.QuizAttempts
                    .GetActiveAttemptAsync(studentId, request.QuizId, ct);

                if (activeAttempt != null)
                {
                    // لو لسه شغال
                    if (!activeAttempt.ExpiresAt.HasValue || activeAttempt.ExpiresAt > DateTime.UtcNow)
                        return _responseHandler.BadRequest<StartQuizAttemptResponse>("Already have active attempt");

                    // لو انتهى → نقفله
                    activeAttempt.Status = QuizAttemptStatus.Abandoned;
                    activeAttempt.SubmittedAt = activeAttempt.ExpiresAt;
                }


                // 8️⃣ Create Attempt
                var now = DateTime.UtcNow;

                // مدة المحاولة بالثواني أو 30 دقيقة افتراضية
                int durationSeconds = quiz.TimeLimitSeconds ?? 30 * 60;
                var expiresAt = now.AddSeconds(durationSeconds);

                // لو الـ Quiz له EndAt محدد مسبقًا
                if (quiz.EndAt.HasValue && expiresAt > quiz.EndAt.Value)
                    expiresAt = quiz.EndAt.Value;

                var attempt = new QuizAttempt
                {
                    StudentId = studentId,
                    QuizId = request.QuizId,
                    StartedAt = now,
                    Status = QuizAttemptStatus.InProgress,
                    ExpiresAt = expiresAt
                };


                // 9️⃣ Save
                await _unitOfWork.QuizAttempts.AddAsync(attempt);
                await _unitOfWork.SaveChangesAsync(ct);


                // 🔟 Response
                var response = new StartQuizAttemptResponse
                {
                    AttemptId = attempt.Id,
                    StartedAt = attempt.StartedAt,
                    ExpiresAt = attempt.ExpiresAt,
                    Success = true,
                    Message = "Attempt started successfully"
                };

                return _responseHandler.Success(response);
            }
            catch (Exception ex)
            {
                return _responseHandler.InternalServerError<StartQuizAttemptResponse>(
                    $"Error: {ex.Message}");
            }
        }
    }
}