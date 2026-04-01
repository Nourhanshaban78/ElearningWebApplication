using E_Learning.Core.Base;
using MediatR;

public record GetQuizAttemptsQuery(int QuizId)
    : IRequest<Response<List<AttemptSummaryDto>>>;

public class AttemptSummaryDto
{
    public int AttemptId { get; set; }
    public Guid StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public decimal? Score { get; set; }
    public bool? IsPassed { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime StartedAt { get; set; }
    public DateTime? SubmittedAt { get; set; }
}