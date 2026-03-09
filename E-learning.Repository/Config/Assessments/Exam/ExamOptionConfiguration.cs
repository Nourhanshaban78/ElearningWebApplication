using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_learning.Core.Entities.Assessments.Exams;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace E_learning.Repository.Config.Assessments.Exam
{
    public class ExamOptionConfiguration : IEntityTypeConfiguration<ExamOption>
    {
        public void Configure(EntityTypeBuilder<ExamOption> builder)
        {
            builder.ToTable("ExamOptions");

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
            builder.HasOne(o => o.ExamQuestion)
                   .WithMany(q => q.ExamOptions)
                   .HasForeignKey(o => o.QuestionId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relationship: Option -> AttemptAnswers
            builder.HasMany(o => o.ExamAttemptAnswers)
                   .WithOne(a => a.ExamOption)
                   .HasForeignKey(a => a.SelectedOptionId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(o => o.QuestionId);

            // Prevent duplicate order for same question
            builder.HasIndex(o => new { o.QuestionId, o.OrderIndex })
                   .IsUnique();
        }
    }
}
