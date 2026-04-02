namespace E_Learning.Core.Features.Quizzes.Commands.SubmitQuizAttempt
{
    public class SubmitQuizAttemptResponse
    {
        public int AttemptId { get; set; }
        public decimal Score { get; set; }
        public bool IsPassed { get; set; }
        public DateTime SubmittedAt { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}