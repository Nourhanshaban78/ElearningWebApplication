using E_Learning.Core.Entities.Assessments.Quiz;
using E_Learning.core.Interfaces.Repositories.Assessments.Quizzes;
using E_Learning.Repository.Data;
using E_Learning.Repository.Repositories.GenericesRepositories;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Repository.Repositories.GenericesRepositories.Assessments.Quizzes
{
    public class QuizAttemptAnswerRepository
        : GenericRepository<QuizAttemptAnswer, int>,
          IQuizAttemptAnswerRepository
    {
        private readonly ELearningDbContext _context;

        public QuizAttemptAnswerRepository(ELearningDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<QuizAttemptAnswer>> GetByAttemptIdAsync(
            int attemptId,
            CancellationToken ct = default)
        {
            return await _context.QuizAttemptAnswers
                .Where(x => x.AttemptId == attemptId)
                .ToListAsync(ct);
        }

        public async Task<QuizAttemptAnswer?> GetByAttemptAndQuestionAsync(
            int attemptId,
            int questionId,
            CancellationToken ct = default)
        {
            return await _context.QuizAttemptAnswers
                .FirstOrDefaultAsync(x =>
                    x.AttemptId == attemptId &&
                    x.QuestionId == questionId,
                    ct);
        }
    }
}