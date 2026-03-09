// Configurations/Courses/CourseConfiguration.cs
using E_learning.Core.Entities.Identity;
using E_Learning.Core.Entities.Academic;
using E_Learning.Core.Entities.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CourseConfiguration
    : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Title)
               .HasMaxLength(200)
               .IsRequired();

        builder.Property(c => c.Slug)
               .HasMaxLength(200)
               .IsRequired();

        builder.HasIndex(c => c.Slug)
               .IsUnique()
               .HasDatabaseName("UQ_Course_Slug");

        builder.Property(c => c.Description)
               .IsRequired();

        builder.Property(c => c.Language)
               .HasMaxLength(10)
               .HasDefaultValue("en");

        builder.Property(c => c.Price)
               .HasColumnType("decimal(10,2)")
               .HasDefaultValue(0);

        builder.Property(c => c.Status)
               .HasMaxLength(20)
               .HasDefaultValue("Draft");

        builder.Property(c => c.IsActive)
               .HasDefaultValue(true);

        builder.Property(c => c.CreatedAt)
               .HasDefaultValueSql("GETUTCDATE()");

        // ─── Soft Delete ─────────────────────────
        builder.Property(c => c.IsDeleted)
               .HasDefaultValue(false);

        // ─── Relations ───────────────────────────

        // Course → Instructor
        builder.HasOne(c => c.Instructor)
           .WithMany()
           .HasForeignKey(c => c.InstructorId)
           .OnDelete(DeleteBehavior.Restrict);

        // ApprovedBy
        builder.HasOne(c => c.ApprovedByUser)
               .WithMany()
               .HasForeignKey(c => c.ApprovedBy)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(c => c.Level)         
            .WithMany()
            .HasForeignKey(c => c.LevelId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);
    }
}