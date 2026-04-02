using E_Learning.Core.Entities.Assessments.Quiz;
using E_Learning.Core.Interfaces.Repositories;

namespace E_Learning.core.Interfaces.Repositories.Assessments.Quizzes
{
    public interface IQuizAttemptRepository : IGenericRepository<QuizAttempt, int>
    {
        Task<QuizAttempt?> GetActiveAttemptAsync(Guid studentId, int quizId, CancellationToken ct = default);
        Task<int> GetAttemptsCountAsync(Guid studentId, int quizId, CancellationToken ct = default);
        Task<QuizAttempt?> GetWithAnswersAsync(int attemptId, CancellationToken ct = default);
        Task<IReadOnlyList<QuizAttempt>> GetByQuizIdAsync(int quizId, CancellationToken ct = default);
        Task<IReadOnlyList<QuizAttempt>> GetByStudentIdAsync(Guid studentId, CancellationToken ct = default);
    }
}