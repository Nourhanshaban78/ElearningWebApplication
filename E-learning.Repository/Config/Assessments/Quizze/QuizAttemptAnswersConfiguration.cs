using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_learning.Core.Entities.Assessments.Quizzes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace E_learning.Repository.Config.Assessments.Quizze
{
    public class QuizAttemptAnswersConfiguration : IEntityTypeConfiguration<QuizAttemptAnswer>
    {
        public void Configure(EntityTypeBuilder<QuizAttemptAnswer> builder)
        {
            builder.ToTable("QuizAttemptAnswers");

            // Primary Key
            builder.HasKey(a => a.Id);

            // Properties
            builder.Property(a => a.TextAnswer)
                   .HasMaxLength(4000);

            // Relationship: Answer -> Attempt
            builder.HasOne(a => a.QuizAttempt)
                   .WithMany(at => at.QuizAttemptAnswers)
                   .HasForeignKey(a => a.AttemptId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relationship: Answer -> Question
            builder.HasOne(a => a.QuizQuestion)
                   .WithMany(q => q.QuizAttemptAnswers)
                   .HasForeignKey(a => a.QuestionId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Relationship: Answer -> Selected Option
            builder.HasOne(a => a.QuizOption)
                   .WithMany()
                   .HasForeignKey(a => a.SelectedOptionId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(a => a.AttemptId);
            builder.HasIndex(a => a.QuestionId);

            // Prevent duplicate answers for the same question in one attempt
            builder.HasIndex(a => new { a.AttemptId, a.QuestionId })
                   .IsUnique();
        }
    }
}
