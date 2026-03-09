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
    public class ExamAttemptAnswerConfiguration : IEntityTypeConfiguration<ExamAttemptAnswer>
    {
        public void Configure(EntityTypeBuilder<ExamAttemptAnswer> builder)
        {
            builder.ToTable("ExamAttemptAnswers");

            // Primary Key
            builder.HasKey(a => a.Id);

            // Properties
            builder.Property(a => a.TextAnswer)
                   .HasMaxLength(4000);

            builder.Property(a => a.Score)
                   .HasColumnType("decimal(6,2)");

            // Relationship: Answer -> ExamAttempt
            builder.HasOne(a => a.ExamAttempt)
                   .WithMany(a => a.ExamAttemptAnswers)
                   .HasForeignKey(a => a.AttemptId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relationship: Answer -> Question
            builder.HasOne(a => a.ExamQuestion)
                   .WithMany(q => q.ExamAttemptAnswers)
                   .HasForeignKey(a => a.QuestionId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Relationship: Answer -> Selected Option
            builder.HasOne(a => a.ExamOption)
                   .WithMany()
                   .HasForeignKey(a => a.SelectedOptionId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(a => a.AttemptId);
            builder.HasIndex(a => a.QuestionId);

            // Prevent duplicate answers for same question in same attempt
            builder.HasIndex(a => new { a.AttemptId, a.QuestionId })
                   .IsUnique();
        }
    }
}
