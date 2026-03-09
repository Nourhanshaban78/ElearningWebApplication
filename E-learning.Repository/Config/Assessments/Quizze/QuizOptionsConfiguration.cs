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
    public class QuizOptionsConfiguration : IEntityTypeConfiguration<QuizOption>
    {
        public void Configure(EntityTypeBuilder<QuizOption> builder)
        {
            builder.ToTable("QuizOptions");

            // Primary Key
            builder.HasKey(o => o.Id);

            // Properties
            builder.Property(o => o.Text)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(o => o.IsCorrect)
                   .HasDefaultValue(false);

            builder.Property(o => o.OrderIndex)
                   .IsRequired();

            // Relationship: Option -> Question
            builder.HasOne(o => o.QuizQuestion)
                   .WithMany(q => q.QuizOptions)
                   .HasForeignKey(o => o.QuestionId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relationship: Option -> AttemptAnswers
            builder.HasMany(o => o.QuizAttemptAnswers)
                   .WithOne(a => a.QuizOption)
                   .HasForeignKey(a => a.SelectedOptionId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(o => o.QuestionId);

            // Prevent duplicate order inside same question
            builder.HasIndex(o => new { o.QuestionId, o.OrderIndex })
                   .IsUnique();
        }
    }
}
