using E_learning.Core.Entities.Identity;
using E_Learning.Core.Entities.Billing;
using E_Learning.Core.Entities.Courses;
using E_Learning.Core.Entities.Enrollment;
using E_Learning.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EnrollmentConfiguration
    : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        builder.ToTable("Enrollments");
        builder.HasKey(e => e.Id);

        builder.HasIndex(e => new { e.StudentId, e.CourseId })
               .IsUnique()
               .HasDatabaseName("UQ_Enrollment_Student_Course");

        builder.Property(e => e.Status)
               .HasConversion<string>()
               .HasMaxLength(20)
               .HasDefaultValue(EnrollmentStatus.NotStarted);

        builder.Property(e => e.ProgressPercentage)
               .HasColumnType("decimal(5,2)")
               .HasDefaultValue(0);

        builder.Property(e => e.EnrolledAt)
               .HasDefaultValueSql("GETUTCDATE()");

        // ─── Soft Delete ─────────────────────────
        builder.Property(e => e.IsDeleted)
               .HasDefaultValue(false);

        // ─── Relations ───────────────────────────

        builder.HasOne(e => e.Student)
               .WithMany()
               .HasForeignKey(e => e.StudentId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Course)
               .WithMany()
               .HasForeignKey(e => e.CourseId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Transaction)
               .WithMany()
               .HasForeignKey(e => e.TransactionId)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.SetNull);
    }
}