using E_Learning.Core.Base;
using E_Learning.Core.Entities.Assessments.Quiz;
using E_Learning.Core.Enums;
using E_Learning.Core.Interfaces.Repositories;
using E_Learning.Core.Repository;
using MediatR;

namespace E_Learning.Core.Features.Quizzes.Commands.SaveAnswer
{
    public class SaveAnswerHandler : IRequestHandler<SaveAnswerCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;

        public SaveAnswerHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
        }

        public async Task<Response<string>> Handle(SaveAnswerCommand request, CancellationToken ct)
        {
            try
            {
                // 1) Get attempt
                var attempt = await _unitOfWork.QuizAttempts.GetByIdAsync(request.AttemptId);
                if (attempt == null)
                    return _responseHandler.NotFound<string>("Attempt not found");

                // 2) Check status
                if (attempt.Status != QuizAttemptStatus.InProgress)
                    return _responseHandler.BadRequest<string>("Attempt is not active");

                // 3) Check time expiration
                if (attempt.ExpiresAt.HasValue && attempt.ExpiresAt <= DateTime.UtcNow)
                    return _responseHandler.BadRequest<string>("Attempt has expired");

                // 4) Check question belongs to same quiz
                var question = await _unitOfWork.QuizQuestions.GetByIdAsync(request.QuestionId);
                if (question == null || question.QuizId != attempt.QuizId)
                    return _responseHandler.BadRequest<string>("Invalid question");

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