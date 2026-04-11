using E_Learning.Core.Entities.Assessments.Quiz;
using E_Learning.Core.Interfaces.Repositories;

namespace E_Learning.core.Interfaces.Repositories.Assessments.Quizzes
{
    public interface IQuizAttemptAnswerRepository
      : IGenericRepository<QuizAttemptAnswer, int>
    {
        Task<IReadOnlyList<QuizAttemptAnswer>> GetByAttemptIdAsync(int attemptId, CancellationToken ct = default);
        Task<QuizAttemptAnswer?> GetByAttemptAndQuestionAsync(
    int attemptId,
    int questionId,
    CancellationToken ct = default);
    }
}