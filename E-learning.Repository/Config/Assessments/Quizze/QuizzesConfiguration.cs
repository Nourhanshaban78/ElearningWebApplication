using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using E_learning.Core.Entities.Assessments.Quizzes;
using E_learning.Core.Enums;

namespace E_learning.Repository.Config.Assessments.Quizze
{
    public class QuizzesConfiguration : IEntityTypeConfiguration<Quiz>
    {
        public void Configure(EntityTypeBuilder<Quiz> builder)
        {
            // Table
            builder.ToTable("Quizzes");

            // Primary Key
            builder.HasKey(q => q.Id);

            // ========================
            // Properties
            // ========================

            builder.Property(q => q.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(q => q.Topic)
                   .HasMaxLength(200);

            builder.Property(q => q.Type)
                   .HasDefaultValue(QuizzesType.Regular);

            builder.Property(q => q.TimePerQuestionSeconds)
                   .HasDefaultValue(30);

            builder.Property(q => q.PassingScore)
                   .HasColumnType("decimal(6,2)")
                   .HasDefaultValue(60);

            builder.Property(q => q.ShuffleQuestions)
                   .HasDefaultValue(true);

            builder.Property(q => q.ShowResultsImmediately)
                   .HasDefaultValue(true);

            builder.Property(q => q.IsActive)
                   .HasDefaultValue(true);

            // ========================
            // Relationships
            // ========================

            // Quiz → Course
            builder.HasOne(q => q.Course)
                   .WithMany(c => c.Quizzes)
                   .HasForeignKey(q => q.CourseId)
                   .OnDelete(DeleteBehavior.NoAction);

            // Quiz → Lesson (optional)
            builder.HasOne(q => q.Lesson)
                   .WithMany(l => l.Quizzes)
                   .HasForeignKey(q => q.LessonId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Quiz → Instructor
            builder.HasOne(q => q.Instructor)
                   .WithMany(i => i.Quizzes)
                   .HasForeignKey(q => q.InstructorId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Quiz → Questions
            builder.HasMany(q => q.QuizQuestions)
                   .WithOne(qq => qq.Quiz)
                   .HasForeignKey(qq => qq.QuizId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Quiz → Attempts
            builder.HasMany(q => q.QuizAttempts)
                   .WithOne(a => a.Quiz)
                   .HasForeignKey(a => a.QuizId)
                   .OnDelete(DeleteBehavior.Cascade);

            // ========================
            // Indexes
            // ========================

            builder.HasIndex(q => q.CourseId);
            builder.HasIndex(q => q.LessonId);
            builder.HasIndex(q => q.InstructorId);
        }
    }
}