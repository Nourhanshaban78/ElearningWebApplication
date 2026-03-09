using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using E_learning.Core.Entities.Assessments.Exams;

namespace E_learning.Repository.Config.Assessments.Exam
{
    public class ExamConfiguration : IEntityTypeConfiguration<Core.Entities.Assessments.Exams.Exam>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.Assessments.Exams.Exam> builder)
        {
            builder.ToTable("Exams");

            // Primary Key
            builder.HasKey(e => e.Id);

            // Properties
            builder.Property(e => e.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(e => e.Instructions)
                   .HasMaxLength(2000);

            builder.Property(e => e.Rules)
                   .HasMaxLength(2000);

            builder.Property(e => e.TechnicalRequirements)
                   .HasMaxLength(2000);

            builder.Property(e => e.EducationLevel)
                   .HasMaxLength(100);

            builder.Property(e => e.SourceFileUrl)
                   .HasMaxLength(1000);

            builder.Property(e => e.TotalMarks)
                   .HasColumnType("decimal(6,2)")
                   .IsRequired();

            builder.Property(e => e.PassingScore)
                   .HasColumnType("decimal(6,2)")
                   .HasDefaultValue(60);

            builder.Property(e => e.MaxAttempts)
                   .HasDefaultValue(1);

            builder.Property(e => e.AIShuffleEnabled)
                   .HasDefaultValue(true);

            builder.Property(e => e.IsActive)
                   .HasDefaultValue(true);

            builder.Property(e => e.DurationSeconds)
                   .IsRequired();

            builder.Property(e => e.ScheduledAt)
                   .IsRequired();

            // Relationship: Exam -> Course
            builder.HasOne(e => e.Course)
                   .WithMany(c => c.Exams)
                   .HasForeignKey(e => e.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relationship: Exam -> Instructor
            builder.HasOne(e => e.Instructor)
                   .WithMany(i => i.Exams)
                   .HasForeignKey(e => e.InstructorId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Relationship: Exam -> Questions
            builder.HasMany(e => e.ExamQuestions)
                   .WithOne(q => q.Exam)
                   .HasForeignKey(q => q.ExamId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relationship: Exam -> Attempts
            builder.HasMany(e => e.ExamAttempts)
                   .WithOne(a => a.Exam)
                   .HasForeignKey(a => a.ExamId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(e => e.CourseId);
            builder.HasIndex(e => e.InstructorId);
            builder.HasIndex(e => e.ScheduledAt);

        }
    }
}
