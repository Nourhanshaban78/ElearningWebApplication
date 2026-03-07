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

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Score)
                   .HasColumnType("decimal(5,2)");

            builder.Property(x => x.Status)
                   .IsRequired();

            builder.Property(x => x.StartedAt)
                   .IsRequired();

            builder.HasOne(x => x.Student)
                   .WithMany()
                   .HasForeignKey(x => x.StudentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Quizzes)
                   .WithMany()
                   .HasForeignKey(x => x.QuizId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.QuizAttemptAnswers)
                   .WithOne()
                   .HasForeignKey("QuizAttemptId")
                   .OnDelete(DeleteBehavior.Cascade);
        
    }
    }
}
