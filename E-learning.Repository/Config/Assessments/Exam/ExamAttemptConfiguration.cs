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

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Score)
                   .HasColumnType("decimal(5,2)");

            builder.Property(x => x.TeacherComment)
                   .HasMaxLength(1000);

            builder.Property(x => x.Status)
                   .IsRequired();

            builder.Property(x => x.StartedAt)
                   .IsRequired();

            builder.HasOne(x => x.Student)
                   .WithMany()
                   .HasForeignKey(x => x.StudentId)
                   .OnDelete(DeleteBehavior.Cascade);

            
            builder.HasOne(x => x.Exams)
                   .WithMany()
                   .HasForeignKey(x => x.ExamId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.User)
                   .WithMany()
                   .HasForeignKey(x => x.ReviewedBy)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.ExamAttemptAnswers)
                   .WithOne()
                   .HasForeignKey("ExamAttemptId")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    
    }
}
