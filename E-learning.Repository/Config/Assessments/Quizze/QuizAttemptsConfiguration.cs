using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_learning.Core.Entities.Assessments.Quizzes;
using E_learning.Core.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace E_learning.Repository.Config.Assessments.Quizze
{
    public class QuizAttemptsConfiguration : IEntityTypeConfiguration<QuizAttempts>
    {
        public void Configure(EntityTypeBuilder<QuizAttempts> builder)
        {
            builder.ToTable("QuizAttempts");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Score)
            .HasColumnType("decimal(5,2)");

            builder.Property(a => a.Status) 
                   .HasMaxLength(20)
                   .HasDefaultValue(QuizAttemptsStatus.InProgress);

            builder.Property(a => a.StartedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(a => a.Students)
                   .WithMany(s=>s.QuizAttempts)
                   .HasForeignKey(a => a.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Quizzes)
                   .WithMany(q => q.QuizAttempts)
                   .HasForeignKey(a => a.QuizId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
