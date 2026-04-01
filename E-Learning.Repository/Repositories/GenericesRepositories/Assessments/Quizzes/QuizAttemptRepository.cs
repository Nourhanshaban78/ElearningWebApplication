using E_Learning.core.Interfaces.Repositories.Assessments.Quizzes;
using E_Learning.Core.Entities.Assessments.Quiz;
using E_Learning.Core.Enums;
using E_Learning.Repository.Data;
using E_Learning.Repository.Repositories.GenericesRepositories;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Repository.Repositories.GenericesRepositories.Assessments.Quizzes
{
    public class QuizAttemptRepository
        : GenericRepository<QuizAttempt, int>, IQuizAttemptRepository
    {
        private readonly ELearningDbContext _context;

        public QuizAttemptRepository(ELearningDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<QuizAttempt?> GetActiveAttemptAsync(Guid studentId, int quizId, CancellationToken ct = default)
        {
            return await _context.QuizAttempts
                .Where(x => x.StudentId == studentId
                         && x.QuizId == quizId
                         && x.Status == QuizAttemptStatus.InProgress)
                .OrderByDescending(x => x.StartedAt)
                .FirstOrDefaultAsync(ct);
        }

        public async Task<int> GetAttemptsCountAsync(Guid studentId, int quizId, CancellationToken ct = default)
        {
            return await _context.QuizAttempts
                .CountAsync(x => x.StudentId == studentId && x.QuizId == quizId, ct);
        }

        public async Task<QuizAttempt?> GetWithAnswersAsync(int attemptId, CancellationToken ct = default)
        {
            return await _context.QuizAttempts
         .Include(a => a.Quiz)                  // تحميل Quiz
             .ThenInclude(q => q.Questions)     // تحميل Questions
                 .ThenInclude(q => q.Options)   // تحميل Options لكل Question
         .Include(a => a.Answers)                // تحميل إجابات Attempt
             .ThenInclude(a => a.SelectedOption)
         .Include(a => a.Answers)
             .ThenInclude(a => a.SelectedOptions)
         .FirstOrDefaultAsync(a => a.Id == attemptId, ct);
        }

        public async Task<IReadOnlyList<QuizAttempt>> GetByQuizIdAsync(int quizId, CancellationToken ct = default)
        {
            return await _context.QuizAttempts
                .Where(x => x.QuizId == quizId)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<QuizAttempt>> GetByStudentIdAsync(Guid studentId, CancellationToken ct = default)
        {
            return await _context.QuizAttempts
                .Where(x => x.StudentId == studentId)
                .ToListAsync(ct);
        }
    }
}