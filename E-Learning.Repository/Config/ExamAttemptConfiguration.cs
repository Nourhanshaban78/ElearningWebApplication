using E_learning.Core.Entities.Identity;
using E_Learning.Core.Entities.Assessments.Exams;
using E_Learning.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ExamAttemptConfiguration
    : IEntityTypeConfiguration<ExamAttempt>
{
    public void Configure(EntityTypeBuilder<ExamAttempt> builder)
    {
        builder.ToTable("ExamAttempts");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Status)
               .HasConversion<string>()
               .HasMaxLength(20)
               .HasDefaultValue(ExamAttemptStatus.InProgress);

        builder.Property(a => a.Score)
               .HasColumnType("decimal(7,2)");

        builder.Property(a => a.StartedAt)
               .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(a => a.IsPublished)
               .HasDefaultValue(false);

        builder.Property(a => a.TeacherComment)
               .HasMaxLength(1000);

        builder.Property(a => a.ReviewDecision)
               .HasMaxLength(20);

        // ─── Relations ───────────────────────────

        // ── ExamAttemptConfiguration ─────────────
        builder.HasOne(a => a.Student)
               .WithMany()
               .HasForeignKey(a => a.StudentId)
               .OnDelete(DeleteBehavior.Restrict);

        // ExamAttemptConfiguration.cs
        builder.HasOne(a => a.Exam)
               .WithMany(e => e.Attempts) 
               .HasForeignKey(a => a.ExamId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a => a.ReviewedByUser)
               .WithMany()
               .HasForeignKey(a => a.ReviewedBy)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.NoAction);
    }
}