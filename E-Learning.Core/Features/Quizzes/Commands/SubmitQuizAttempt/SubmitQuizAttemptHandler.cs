using E_Learning.Core.Base;
using E_Learning.Core.Entities.Assessments.Quiz;
using E_Learning.Core.Enums;
using E_Learning.Core.Interfaces.Repositories;
using E_Learning.Core.Repository;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace E_Learning.Core.Features.Quizzes.Commands.SubmitQuizAttempt
{
    public class SubmitQuizAttemptHandler : IRequestHandler<SubmitQuizAttemptCommand, Response<SubmitQuizAttemptResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ResponseHandler _responseHandler;

        public SubmitQuizAttemptHandler(
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor,
            ResponseHandler responseHandler)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _responseHandler = responseHandler;
        }

        public async Task<Response<SubmitQuizAttemptResponse>> Handle(SubmitQuizAttemptCommand request, CancellationToken ct)
        {
            // 1️⃣ الحصول على StudentId من Claims
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null)
                return _responseHandler.Unauthorized<SubmitQuizAttemptResponse>();

            var studentIdClaim =
                user.FindFirst(ClaimTypes.NameIdentifier) ??
                user.FindFirst("sub") ??
                user.FindFirst("userId");

            if (studentIdClaim == null)
                return _responseHandler.Unauthorized<SubmitQuizAttemptResponse>();

            var studentId = Guid.Parse(studentIdClaim.Value);

            // 2️⃣ جلب Attempt مع الأسئلة والإجابات
            var attempt = await _unitOfWork.QuizAttempts.GetWithAnswersAsync(request.AttemptId, ct);
            if (attempt == null)
                return _responseHandler.NotFound<SubmitQuizAttemptResponse>("Attempt not found");

            if (attempt.StudentId != studentId)
                return _responseHandler.Forbidden<SubmitQuizAttemptResponse>("This attempt is not yours");

            if (attempt.Status != QuizAttemptStatus.InProgress)
                return _responseHandler.BadRequest<SubmitQuizAttemptResponse>("This attempt has already been submitted");

            // 3️⃣ Check ExpiresAt
            if (attempt.ExpiresAt.HasValue && attempt.ExpiresAt <= DateTime.UtcNow)
            {
                attempt.Status = QuizAttemptStatus.Abandoned;
                await _unitOfWork.SaveChangesAsync(ct);
                return _responseHandler.BadRequest<SubmitQuizAttemptResponse>("Attempt has expired");
            }

            var quiz = attempt.Quiz;

            // 3️⃣ حساب النقاط الإجمالية من الإجابات الموجودة مسبقًا
            decimal totalScore = 0;

            foreach (var question in quiz.Questions)
            {
                var attemptAnswer = attempt.Answers.FirstOrDefault(a => a.QuestionId == question.Id);
                if (attemptAnswer == null)
                    continue; // الطالب ما جاوبش السؤال

                switch (question.Type)
                {
                    case "MultipleChoice":
                    case "TrueFalse":
                        if (attemptAnswer.SelectedOption != null && attemptAnswer.SelectedOption.IsCorrect)
                            totalScore += question.Points;
                        break;

                    case "MultipleSelect":
                        if (attemptAnswer.SelectedOptions != null && attemptAnswer.SelectedOptions.Any())
                        {
                            var correctIds = question.Options.Where(o => o.IsCorrect).Select(o => o.Id).ToHashSet();
                            var selectedIds = attemptAnswer.SelectedOptions.Select(o => o.Id).ToHashSet();

                            if (selectedIds.SetEquals(correctIds))
                                totalScore += question.Points;
                        }
                        break;

                    case "Text":
                    case "Essay":
                        attemptAnswer.NeedsReview = true; // اجابات النصية للمراجعة
                        break;

                    default:
                        attemptAnswer.NeedsReview = true;
                        break;
                }
            }

            // 4️⃣ تحديث Attempt
            attempt.SubmittedAt = DateTime.UtcNow;
            attempt.Score = totalScore;
            attempt.IsPassed = totalScore >= quiz.PassingScore;
            attempt.Status = QuizAttemptStatus.Submitted;

            // 5️⃣ حفظ Attempt فقط (بدون عمل Insert جديد على QuizAttemptAnswers)
            try
            {
                await _unitOfWork.SaveChangesAsync(ct);
            }
            catch (Exception ex)
            {
                return _responseHandler.BadRequest<SubmitQuizAttemptResponse>($"Failed to submit attempt: {ex.Message}");
            }

            // 6️⃣ إنشاء DTO للرد
            var responseData = new SubmitQuizAttemptResponse
            {
                AttemptId = attempt.Id,
                Score = totalScore,
                IsPassed = attempt.IsPassed ?? false,
                SubmittedAt = attempt.SubmittedAt ?? DateTime.UtcNow,
                Message = "Quiz submitted successfully. Text answers will be reviewed by the instructor."
            };

            return _responseHandler.Success(responseData);
        }
    }
}