using E_Learning.Core.Base;
using E_Learning.Core.Entities.Assessments.Quiz;
using E_Learning.Core.Enums;
using E_Learning.Core.Interfaces.Repositories;
using E_Learning.Core.Repository;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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

            var studentIdClaim = user.FindFirst("sub") ?? user.FindFirst("userId");
            if (studentIdClaim == null)
                return _responseHandler.Unauthorized<SubmitQuizAttemptResponse>();

            var studentId = Guid.Parse(studentIdClaim.Value);

            // 2️⃣ جلب Attempt مع الأسئلة والإجابات
            var attempt = await _unitOfWork.QuizAttempts.GetWithAnswersAsync(request.AttemptId, ct);
            if (attempt == null || attempt.StudentId != studentId)
                return _responseHandler.NotFound<SubmitQuizAttemptResponse>("Attempt not found");

            if (attempt.Status != QuizAttemptStatus.InProgress)
                return _responseHandler.BadRequest<SubmitQuizAttemptResponse>("This attempt has already been submitted");

            var quiz = attempt.Quiz;

            // 3️⃣ حساب النقاط الإجمالية لكل سؤال
            decimal totalScore = 0;

            foreach (var question in quiz.Questions)
            {
                var userAnswer = request.Answers.FirstOrDefault(a => a.QuestionId == question.Id);
                if (userAnswer == null) continue;

                var attemptAnswer = new QuizAttemptAnswer
                {
                    AttemptId = attempt.Id,
                    QuestionId = question.Id
                };

                switch (question.Type)
                {
                    case "MultipleChoice":
                    case "TrueFalse":
                        // سؤال بإجابة واحدة
                        if (userAnswer.SelectedOptionIds != null && userAnswer.SelectedOptionIds.Count == 1)
                        {
                            var optionId = userAnswer.SelectedOptionIds.First();
                            var selectedOption = question.Options.FirstOrDefault(o => o.Id == optionId);
                            attemptAnswer.SelectedOption = selectedOption;

                            if (selectedOption != null && selectedOption.IsCorrect)
                                totalScore += question.Points;
                        }
                        break;

                    case "MultipleSelect":
                        // سؤال بإجابات متعددة
                        if (userAnswer.SelectedOptionIds != null && userAnswer.SelectedOptionIds.Any())
                        {
                            var selectedOptions = question.Options
                                .Where(o => userAnswer.SelectedOptionIds.Contains(o.Id))
                                .ToList();

                            attemptAnswer.SelectedOptions = selectedOptions;

                            var correctIds = question.Options.Where(o => o.IsCorrect).Select(o => o.Id).OrderBy(i => i).ToList();
                            if (userAnswer.SelectedOptionIds.OrderBy(i => i).SequenceEqual(correctIds))
                                totalScore += question.Points;
                        }
                        break;

                    case "Text":
                    case "Essay":
                        // الأسئلة النصية للمراجعة
                        attemptAnswer.NeedsReview = true;
                        break;

                    default:
                        // أي نوع جديد مستقبلي
                        attemptAnswer.NeedsReview = true;
                        break;
                }

                attempt.Answers.Add(attemptAnswer);
            }

            // 4️⃣ تحديث Attempt
            attempt.SubmittedAt = DateTime.UtcNow;
            attempt.Score = totalScore;
            attempt.IsPassed = totalScore >= quiz.PassingScore;
            attempt.Status = QuizAttemptStatus.Submitted;

            // 5️⃣ حفظ Attempt
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