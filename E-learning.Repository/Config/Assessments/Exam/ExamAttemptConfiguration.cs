using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_learning.Core.Entities.Assessments.Exams;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using E_learning.Core.Enums;

namespace E_learning.Repository.Config.Assessments.Exam
{
    public class ExamAttemptConfiguration : IEntityTypeConfiguration<ExamAttempt>
    {
        public void Configure(EntityTypeBuilder<ExamAttempt> builder)
        {
            builder.ToTable("ExamAttempts");

            // Primary Key
            builder.HasKey(a => a.Id);

            // Properties
            builder.Property(a => a.Score)
                   .HasColumnType("decimal(6,2)");

            builder.Property(a => a.TeacherComment)
                   .HasMaxLength(2000);

            builder.Property(a => a.Status)
                   .HasDefaultValue(ExamAttemptsStatus.InProgress);

            builder.Property(a => a.IsPublished)
                   .HasDefaultValue(false);

            builder.Property(a => a.StartedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Relationship: Attempt -> Student
            builder.HasOne(a => a.Student)
                   .WithMany(s => s.ExamAttempts)
                   .HasForeignKey(a => a.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Relationship: Attempt -> Exam
            builder.HasOne(a => a.Exam)
                   .WithMany(e => e.ExamAttempts)
                   .HasForeignKey(a => a.ExamId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relationship: Attempt -> Answers
            builder.HasMany(a => a.ExamAttemptAnswers)
                   .WithOne(ans => ans.ExamAttempt)
                   .HasForeignKey(ans => ans.AttemptId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relationship: Attempt -> SelectedOption
            builder.HasOne(a => a.SelectedOption)
                   .WithMany()
                   .HasForeignKey(a => a.SelectedOptionId)
                   .OnDelete(DeleteBehavior.Restrict);
         
            builder.HasOne(e => e.Reviewer)
                .WithMany()
             .HasForeignKey(e => e.ReviewedBy)
                     .OnDelete(DeleteBehavior.NoAction);


            // Indexes
            builder.HasIndex(a => a.StudentId);
            builder.HasIndex(a => a.ExamId);
            builder.HasIndex(a => new { a.StudentId, a.ExamId });
        }
    
    }
}
