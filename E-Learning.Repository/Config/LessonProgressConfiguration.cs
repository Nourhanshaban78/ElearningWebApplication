using E_Learning.Core.Entities.Courses;
using E_Learning.Core.Entities.Enrollment;
using E_Learning.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class LessonProgressConfiguration
    : IEntityTypeConfiguration<LessonProgress>
{
    public void Configure(EntityTypeBuilder<LessonProgress> builder)
    {
        builder.ToTable("LessonProgress");
        builder.HasKey(lp => lp.Id);

        builder.HasIndex(lp => new { lp.EnrollmentId, lp.LessonId })
               .IsUnique()
               .HasDatabaseName("UQ_LessonProgress_Enrollment_Lesson");

        builder.Property(lp => lp.Status)
               .HasConversion<string>()
               .HasMaxLength(20)
               .HasDefaultValue(LessonProgressStatus.NotStarted);

        builder.Property(lp => lp.WatchedSeconds)
               .HasDefaultValue(0);

        builder.Property(lp => lp.LastAccessedAt)
               .HasDefaultValueSql("GETUTCDATE()");

        // ─── Relations ───────────────────────────

        // LessonProgressConfiguration.cs
        builder.HasOne(lp => lp.Enrollment)   
               .WithMany()
               .HasForeignKey(lp => lp.EnrollmentId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(lp => lp.Lesson)   
               .WithMany()
               .HasForeignKey(lp => lp.LessonId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}