// using E_learning.Core.Entities.Assessments.Assignments;
using E_learning.Core.Entities.Assessments.Exams;
using E_learning.Core.Entities.Assessments.Quizzes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_learning.Repository.Config
{
    // ─── Quizzes ──────────────────────────────────────────────────────────────

    public class QuizConfiguration : IEntityTypeConfiguration<Quizzes>
    {
        public void Configure(EntityTypeBuilder<Quizzes> builder)
        {
            // Course (1) → (Many) Quizzes
            builder.HasOne(q => q.Courses)
                   .WithMany(c => c.Quizzes)
                   .HasForeignKey(q => q.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Lesson (1) → (Many) Quizzes (optional)
            builder.HasOne(q => q.Lessons)
                   .WithMany(l => l.Quizzes)
                   .HasForeignKey(q => q.LessonId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }

    public class QuizAttemptConfiguration : IEntityTypeConfiguration<QuizAttempts>
    {
        public void Configure(EntityTypeBuilder<QuizAttempts> builder)
        {
            // AppUser (1) → (Many) QuizAttempts
            builder.HasOne(a => a.Students)
                   .WithMany(u => u.QuizAttempts)
                   .HasForeignKey(a => a.StudentId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Quiz (1) → (Many) QuizAttempts
            builder.HasOne(a => a.Quizzes)
                   .WithMany(q => q.QuizAttempts)
                   .HasForeignKey(a => a.QuizId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class QuizAttemptAnswerConfiguration : IEntityTypeConfiguration<QuizAttemptAnswers>
    {
        public void Configure(EntityTypeBuilder<QuizAttemptAnswers> builder)
        {
            // QuizAttempt (1) → (Many) QuizAttemptAnswers
            builder.HasOne(a => a.QuizAttempts)
                   .WithMany(at => at.QuizAttemptAnswers)
                   .HasForeignKey(a => a.AttemptId)
                   .OnDelete(DeleteBehavior.Cascade);

            // QuizQuestion (1) → (Many) QuizAttemptAnswers
            builder.HasOne(a => a.QuizQuestions)
                   .WithMany(q => q.QuizAttemptAnswers)
                   .HasForeignKey(a => a.QuestionId)
                   .OnDelete(DeleteBehavior.NoAction);

            // QuizOption (1) → (Many) QuizAttemptAnswers (optional)
            builder.HasOne(a => a.QuizOptions)
                   .WithMany(o => o.QuizAttemptAnswers)
                   .HasForeignKey(a => a.SelectedOption)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }

    // ─── Exams ────────────────────────────────────────────────────────────────

    public class ExamAttemptsConfiguration : IEntityTypeConfiguration<ExamAttempts>
    {
        public void Configure(EntityTypeBuilder<ExamAttempts> builder)
        {
            // AppUser (Student) (1) → (Many) ExamAttempts
            builder.HasOne(e => e.Student)
                   .WithMany(u => u.ExamAttempts)
                   .HasForeignKey(e => e.StudentId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Exam (1) → (Many) ExamAttempts
            builder.HasOne(e => e.Exams)
                   .WithMany(ex => ex.ExamAttempts)
                   .HasForeignKey(e => e.ExamId)
                   .OnDelete(DeleteBehavior.NoAction);

            // AppUser (Reviewer/Admin) (1) → (Many) ExamAttempts (optional)
            builder.HasOne(e => e.User)
                   .WithMany()
                   .HasForeignKey(e => e.ReviewedBy)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class ExamAttemptAnswerConfiguration : IEntityTypeConfiguration<ExamAttemptAnswers>
    {
        public void Configure(EntityTypeBuilder<ExamAttemptAnswers> builder)
        {
            // ExamAttempt (1) → (Many) ExamAttemptAnswers
            builder.HasOne(a => a.ExamAttempts)
                   .WithMany(at => at.ExamAttemptAnswers)
                   .HasForeignKey(a => a.AttemptId)
                   .OnDelete(DeleteBehavior.Cascade);

            // ExamQuestion (1) → (Many) ExamAttemptAnswers
            builder.HasOne(a => a.ExamQuestions)
                   .WithMany(q => q.ExamAttemptAnswers)
                   .HasForeignKey(a => a.QuestionId)
                   .OnDelete(DeleteBehavior.NoAction);

            // ExamOption (1) → (Many) ExamAttemptAnswers (optional)
            builder.HasOne(a => a.ExamOptions)
                   .WithMany(o => o.ExamAttemptAnswers)
                   .HasForeignKey(a => a.SelectedOption)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }

    // ─── Assignments ──────────────────────────────────────────────────────────

    /*
    public class AssignmentSubmissionConfiguration : IEntityTypeConfiguration<AssignmentSubmissions>
    {
        public void Configure(EntityTypeBuilder<AssignmentSubmissions> builder)
        {
            builder.HasKey(s => s.Id);

            // Unique constraint: one submission per student per assignment
            builder.HasIndex(s => new { s.AssignmentId, s.StudentId })
                   .IsUnique();

            // Assignment (1) → (Many) AssignmentSubmissions
            builder.HasOne(s => s.Assignments)
                   .WithMany(a => a.AssignmentSubmissions)
                   .HasForeignKey(s => s.AssignmentId)
                   .OnDelete(DeleteBehavior.Cascade);

            // AppUser (Student) (1) → (Many) AssignmentSubmissions
            builder.HasOne(s => s.Students)
                   .WithMany()
                   .HasForeignKey(s => s.StudentId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
    */
}
