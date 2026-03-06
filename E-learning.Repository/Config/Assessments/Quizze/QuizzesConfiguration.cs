using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using E_learning.Core.Entities.Assessments.Quizzes;
using E_learning.Core.Enums;

namespace E_learning.Repository.Config.Assessments.Quizze
{
    public class QuizzesConfiguration : IEntityTypeConfiguration<Quizzes>
    {
        public void Configure(EntityTypeBuilder<Quizzes> builder)
        {
            builder.ToTable("Quizzes");

            builder.HasKey(q => q.Id);

              

            builder.Property(q => q.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(q => q.Topic)
                   .HasMaxLength(200);

            builder.Property(q => q.Type)
                   .HasMaxLength(20)
                   .HasDefaultValue(QuizzesType.Regular);

            builder.Property(q => q.TimePerQuestionSeconds)
                   .HasDefaultValue(30);

            builder.Property(q => q.PassingScore)
                   .HasColumnType("decimal(5,2)")
                   .HasDefaultValue(60);

            builder.Property(q => q.MaxAttempts)
                   .HasDefaultValue(3);

            builder.Property(q => q.ShuffleQuestions)
                   .HasDefaultValue(true);

            builder.Property(q => q.ShowResultsImmediately)
                   .HasDefaultValue(true);

            builder.Property(q => q.IsActive)
                   .HasDefaultValue(true);

            builder.HasOne(q => q.Courses)
                   .WithMany(c => c.Quizzes)
                   .HasForeignKey(q => q.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(q => q.Lessons)
                   .WithMany(l => l.Quizzes)
                   .HasForeignKey(q => q.LessonId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
