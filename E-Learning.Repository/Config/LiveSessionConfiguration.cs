using E_learning.Core.Entities.Identity;
using E_Learning.Core.Entities.Courses;
using E_Learning.Core.Entities.LiveSessions;
using E_Learning.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class LiveSessionConfiguration
    : IEntityTypeConfiguration<LiveSession>
{
    public void Configure(EntityTypeBuilder<LiveSession> builder)
    {
        builder.ToTable("LiveSessions");
        builder.HasKey(ls => ls.Id);

        builder.Property(ls => ls.Title)
               .HasMaxLength(200)
               .IsRequired();

        builder.Property(ls => ls.Description)
               .HasMaxLength(1000);

        builder.Property(ls => ls.RoomName)
               .HasMaxLength(200)
               .IsRequired();

        builder.Property(ls => ls.RecordingUrl)
               .HasMaxLength(500);

        builder.Property(ls => ls.DurationMinutes)
               .HasDefaultValue(60);

        builder.Property(ls => ls.IsRecorded)
               .HasDefaultValue(true);

        builder.Property(ls => ls.IsVisibleToStudents)
               .HasDefaultValue(false);

        builder.Property(ls => ls.Status)
               .HasConversion<string>()
               .HasMaxLength(20)
               .HasDefaultValue(LiveSessionStatus.Scheduled);

        builder.Property(ls => ls.CreatedAt)
               .HasDefaultValueSql("GETUTCDATE()");

        // ─── Relations ───────────────────────────

        builder.HasOne(ls => ls.Instructor)
               .WithMany()
               .HasForeignKey(ls => ls.InstructorId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ls => ls.Course)
               .WithMany()
               .HasForeignKey(ls => ls.CourseId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}