using E_Learning.Core.Entities.LiveSessions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class LiveSessionAttendeeConfiguration
    : IEntityTypeConfiguration<LiveSessionAttendee>
{
    public void Configure(EntityTypeBuilder<LiveSessionAttendee> builder)
    {
        builder.ToTable("LiveSessionAttendees");
        builder.HasKey(la => la.Id);

        builder.HasIndex(la => new { la.SessionId, la.StudentId })
               .IsUnique()
               .HasDatabaseName("UQ_Attendee_Session_Student");

        builder.Property(la => la.JoinedAt)
               .HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne(la => la.Session)
               .WithMany()
               .HasForeignKey(la => la.SessionId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(la => la.Student)
               .WithMany()
               .HasForeignKey(la => la.StudentId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}