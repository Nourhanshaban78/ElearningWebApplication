using E_Learning.Core.Base;
using MediatR;

public record GetAttemptByIdQuery(int AttemptId)
    : IRequest<Response<AttemptDetailResponse>>;

public class AttemptDetailResponse
{
    public int AttemptId { get; set; }
    public decimal? Score { get; set; }
    public bool? IsPassed { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? SubmittedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public string Status { get; set; } = string.Empty;
    public List<AnswerDetailDto> Answers { get; set; } = new();
}

public class AnswerDetailDto
{
    public int QuestionId { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public int? SelectedOptionId { get; set; }
    public string? SelectedOptionText { get; set; }
    public bool? IsCorrect { get; set; }
    public string? TextAnswer { get; set; }
    public bool NeedsReview { get; set; }
}