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
    public class ExamAttemptAnswerConfiguration : IEntityTypeConfiguration<ExamAttemptAnswers>
    {
        public void Configure(EntityTypeBuilder<ExamAttemptAnswers> builder)
        {
            builder.ToTable("ExamAttemptAnswers");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Score)
                   .HasColumnType("decimal(5,2)");

            builder.HasOne(a => a.ExamAttempts)
                   .WithMany(a => a.ExamAttemptAnswers)
                   .HasForeignKey(a => a.AttemptId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.ExamQuestions)
                   .WithMany(e=>e.ExamAttemptAnswers)
                   .HasForeignKey(a => a.QuestionId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.ExamOptions)
                   .WithMany(a=>a.ExamAttemptAnswers)
                   .HasForeignKey(a => a.SelectedOption)
                   .OnDelete(DeleteBehavior.SetNull);
            builder.Property(a => a.TextAnswer)
                        .HasMaxLength(int.MaxValue);
        }
    }
}
