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
    public class ExamAttemptConfiguration : IEntityTypeConfiguration<ExamAttempts>
    {
        public void Configure(EntityTypeBuilder<ExamAttempts> builder)
        {
            builder.ToTable("ExamAttempts");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Score)
                   .HasColumnType("decimal(7,2)");

            builder.Property(a => a.Status) 
                   .HasMaxLength(20)
                   .HasDefaultValue(ExamAttemptsStatus.InProgress);

            builder.Property(a => a.StartedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(a => a.Student)
                   .WithMany(s=>s.ExamAttempts)
                   .HasForeignKey(a => a.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Exams)
                   .WithMany(e => e.ExamAttempts)
                   .HasForeignKey(a => a.ExamId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.Property(a => a.IsPublished)
                  .HasDefaultValue(false);
            builder.Property(a => a.TeacherComment)
                  .HasMaxLength(1000);
        }
    }
}
