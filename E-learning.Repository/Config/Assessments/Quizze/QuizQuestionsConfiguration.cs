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
    public class QuizQuestionsConfiguration : IEntityTypeConfiguration<QuizQuestion>
    {
        public void Configure(EntityTypeBuilder<QuizQuestion> builder)
        {
            builder.ToTable("QuizQuestions");

            // Primary Key
            builder.HasKey(q => q.Id);

            // Properties
            builder.Property(q => q.Text)
                   .IsRequired()
                   .HasMaxLength(2000);

            builder.Property(q => q.Type)
                   .IsRequired();

            builder.Property(q => q.Points)
                   .HasColumnType("decimal(6,2)")
                   .HasDefaultValue(1);

            builder.Property(q => q.IsAIGenerated)
                   .HasDefaultValue(false);

            builder.Property(q => q.OrderIndex)
                   .IsRequired();

            // Relationship: Question -> Quiz
            builder.HasOne(q => q.Quiz)
                   .WithMany(qz => qz.QuizQuestions)
                   .HasForeignKey(q => q.QuizId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relationship: Question -> Options
            builder.HasMany(q => q.QuizOptions)
                   .WithOne(o => o.QuizQuestion)
                   .HasForeignKey(o => o.QuestionId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relationship: Question -> AttemptAnswers
            builder.HasMany(q => q.QuizAttemptAnswers)
                   .WithOne(a => a.QuizQuestion)
                   .HasForeignKey(a => a.QuestionId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(q => q.QuizId);

            // Prevent duplicate order inside same quiz
            builder.HasIndex(q => new { q.QuizId, q.OrderIndex })
                   .IsUnique();
        }
    }
}
