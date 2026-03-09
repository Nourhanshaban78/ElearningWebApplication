using E_Learning.Core.Entities.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CourseAnalyticsSnapshotConfiguration
    : IEntityTypeConfiguration<CourseAnalyticsSnapshot>
{
    public void Configure(EntityTypeBuilder<CourseAnalyticsSnapshot> builder)
    {
        builder.ToTable("CourseAnalyticsSnapshots");
        builder.HasKey(s => s.Id);

        builder.HasIndex(s => new { s.CourseId, s.SnapshotDate })
               .IsUnique()
               .HasDatabaseName("UQ_Snapshot_Course_Date");

        builder.Property(s => s.CompletionRate)
               .HasColumnType("decimal(5,2)");

        builder.Property(s => s.AverageGrade)
               .HasColumnType("decimal(5,2)");

        builder.Property(s => s.QuizPassRate)
               .HasColumnType("decimal(5,2)");

        builder.Property(s => s.ExamPassRate)
               .HasColumnType("decimal(5,2)");

        builder.Property(s => s.TotalRevenue)
               .HasColumnType("decimal(10,2)")
               .HasDefaultValue(0);

        builder.Property(s => s.CreatedAt)
               .HasDefaultValueSql("GETUTCDATE()");

        // Snapshot → Course
        builder.HasOne(s => s.Course)
               .WithMany()
               .HasForeignKey(s => s.CourseId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}