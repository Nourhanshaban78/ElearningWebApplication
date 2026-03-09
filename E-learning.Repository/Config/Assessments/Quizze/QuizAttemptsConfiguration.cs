using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_learning.Core.Entities.Assessments.Quizzes;
using E_learning.Core.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace E_learning.Repository.Config.Assessments.Quizze
{
    public class QuizAttemptsConfiguration : IEntityTypeConfiguration<QuizAttempt>
    {
        public void Configure(EntityTypeBuilder<QuizAttempt> builder)
        {
            builder.ToTable("QuizAttempts");

            // Primary Key
            builder.HasKey(a => a.Id);

            // Properties
            builder.Property(a => a.StartedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(a => a.Score)
                   .HasColumnType("decimal(6,2)");

            builder.Property(a => a.Status)
                   .HasDefaultValue(QuizAttemptsStatus.InProgress);

            // Relationship: Attempt -> Student
            builder.HasOne(a => a.Student)
                   .WithMany(s => s.QuizAttempts)
                   .HasForeignKey(a => a.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Relationship: Attempt -> Quiz
            builder.HasOne(a => a.Quiz)
                   .WithMany(q => q.QuizAttempts)
                   .HasForeignKey(a => a.QuizId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relationship: Attempt -> Answers
            builder.HasMany(a => a.QuizAttemptAnswers)
                   .WithOne(ans => ans.QuizAttempt)
                   .HasForeignKey(ans => ans.AttemptId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(a => a.StudentId);
            builder.HasIndex(a => a.QuizId);

            // Prevent duplicate attempts tracking optimization
            builder.HasIndex(a => new { a.StudentId, a.QuizId });

        }
    }
}
