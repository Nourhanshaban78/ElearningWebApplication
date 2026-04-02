namespace E_Learning.Core.Features.Quizzes.Commands.StartQuizAttempt
{
    public class StartQuizAttemptResponse
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public int AttemptId { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }
}