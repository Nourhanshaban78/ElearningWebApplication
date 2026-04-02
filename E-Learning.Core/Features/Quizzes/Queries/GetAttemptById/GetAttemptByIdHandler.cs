using E_Learning.Core.Base;
using E_Learning.Core.Repository;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

public class GetAttemptByIdHandler : IRequestHandler<GetAttemptByIdQuery, Response<AttemptDetailResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ResponseHandler _responseHandler;

    public GetAttemptByIdHandler(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor,
        ResponseHandler responseHandler)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
        _responseHandler = responseHandler;
    }

    public async Task<Response<AttemptDetailResponse>> Handle(GetAttemptByIdQuery request, CancellationToken ct)
    {
        // 1) Get StudentId من التوكن
        var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var studentId))
            return _responseHandler.Unauthorized<AttemptDetailResponse>();

        // 2) Get Attempt
        var attempt = await _unitOfWork.QuizAttempts.GetWithAnswersAsync(request.AttemptId, ct);
        if (attempt == null)
            return _responseHandler.NotFound<AttemptDetailResponse>("Attempt not found");

        // 3) تأكد إن الـ Attempt بتاعته
        if (attempt.StudentId != studentId)
            return _responseHandler.Forbidden<AttemptDetailResponse>("This attempt is not yours");

        // 4) Map Response
        var response = new AttemptDetailResponse
        {
            AttemptId = attempt.Id,
            Score = attempt.Score,
            IsPassed = attempt.IsPassed,
            StartedAt = attempt.StartedAt,
            SubmittedAt = attempt.SubmittedAt,
            ExpiresAt = attempt.ExpiresAt,
            Status = attempt.Status.ToString(),
            Answers = attempt.Answers.Select(a => new AnswerDetailDto
            {
                QuestionId = a.QuestionId,
                QuestionText = a.Question?.Text ?? "",
                SelectedOptionId = a.SelectedOptionId,
                SelectedOptionText = a.SelectedOption?.Text,
                IsCorrect = a.IsCorrect,
                TextAnswer = a.TextAnswer,
                NeedsReview = a.NeedsReview
            }).ToList()
        };

        return _responseHandler.Success(response);
    }
}