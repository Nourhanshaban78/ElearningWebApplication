using E_Learning.Core.Base;
using E_Learning.Core.Entities.Assessments.Quiz;
using E_Learning.Core.Enums;
using E_Learning.Core.Interfaces.Repositories;
using E_Learning.Core.Repository;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace E_Learning.Core.Features.Quizzes.Commands.SaveAnswer
{
    public class SaveAnswerHandler : IRequestHandler<SaveAnswerCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SaveAnswerHandler(
           IUnitOfWork unitOfWork,
           ResponseHandler responseHandler,
        IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _httpContextAccessor = httpContextAccessor; // ← ناقص السطر ده!
        }
        public async Task<Response<string>> Handle(SaveAnswerCommand request, CancellationToken ct)
        {
            try
            {
                // 1) Get attempt
                var attempt = await _unitOfWork.QuizAttempts.GetByIdAsync(request.AttemptId);
                if (attempt == null)
                    return _responseHandler.NotFound<string>("Attempt not found");

                // بعد ما تجيبي الـ attempt
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var studentId))
                    return _responseHandler.Unauthorized<string>();

                if (attempt.StudentId != studentId)
                    return _responseHandler.Forbidden<string>("This attempt is not yours");

                // 2) Check status
                if (attempt.Status != QuizAttemptStatus.InProgress)
                    return _responseHandler.BadRequest<string>("Attempt is not active");

                // 3) Check time expiration
                if (attempt.ExpiresAt.HasValue && attempt.ExpiresAt <= DateTime.UtcNow)
                {
                    attempt.Status = QuizAttemptStatus.Abandoned;
                    await _unitOfWork.SaveChangesAsync(ct);
                    return _responseHandler.BadRequest<string>("Attempt has expired");
                }

                // 4) Check question belongs to same quiz
                var question = await _unitOfWork.QuizQuestions.GetByIdAsync(request.QuestionId);
                if (question == null || question.QuizId != attempt.QuizId)
                    return _responseHandler.BadRequest<string>("Invalid question");


                if (request.SelectedOptionId.HasValue)
                {
                    var option = await _unitOfWork.QuizOptions.GetByIdAsync(request.SelectedOptionId.Value);
                    if (option == null || option.QuestionId != request.QuestionId)
                        return _responseHandler.BadRequest<string>("Invalid option");
                }

                // 5) Check existing answer
                var existingAnswer = await _unitOfWork.QuizAttemptAnswers
                    .GetByAttemptAndQuestionAsync(request.AttemptId, request.QuestionId, ct);

                if (existingAnswer != null)
                {
                    // Update existing
                    existingAnswer.SelectedOptionId = request.SelectedOptionId;
                    existingAnswer.TextAnswer = request.AnswerText;
                }
                else
                {
                    // Insert new
                    var answer = new QuizAttemptAnswer
                    {
                        AttemptId = request.AttemptId,
                        QuestionId = request.QuestionId,
                        SelectedOptionId = request.SelectedOptionId,
                        TextAnswer = request.AnswerText
                    };

                    await _unitOfWork.QuizAttemptAnswers.AddAsync(answer);
                }

                await _unitOfWork.SaveChangesAsync(ct);

                return _responseHandler.Success("Answer saved successfully");
            }
            catch (Exception ex)
            {
                return _responseHandler.InternalServerError<string>(ex.Message);
            }
        }
    }
}