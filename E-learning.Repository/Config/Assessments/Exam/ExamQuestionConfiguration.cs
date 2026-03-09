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
    public class ExamQuestionConfiguration : IEntityTypeConfiguration<ExamQuestion>
    {
        public void Configure(EntityTypeBuilder<ExamQuestion> builder)
        {
            builder.ToTable("ExamQuestions");

            // Primary Key
            builder.HasKey(q => q.Id);

            // Properties
            builder.Property(q => q.Text)
                   .IsRequired()
                   .HasMaxLength(2000);

            builder.Property(q => q.Type)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(q => q.Points)
                   .HasColumnType("decimal(6,2)")
                   .IsRequired();

            builder.Property(q => q.IsAIGenerated)
                   .HasDefaultValue(false);

            builder.Property(q => q.OrderIndex)
                   .IsRequired();

            // Relationship: Question -> Exam
            builder.HasOne(q => q.Exam)
                   .WithMany(e => e.ExamQuestions)
                   .HasForeignKey(q => q.ExamId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relationship: Question -> Options
            builder.HasMany(q => q.ExamOptions)
                   .WithOne(o => o.ExamQuestion)
                   .HasForeignKey(o => o.QuestionId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relationship: Question -> AttemptAnswers
            builder.HasMany(q => q.ExamAttemptAnswers)
                   .WithOne(a => a.ExamQuestion)
                   .HasForeignKey(a => a.QuestionId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(q => q.ExamId);

            // Prevent duplicate order in same exam
            builder.HasIndex(q => new { q.ExamId, q.OrderIndex })
                   .IsUnique();

        }
    }
}
