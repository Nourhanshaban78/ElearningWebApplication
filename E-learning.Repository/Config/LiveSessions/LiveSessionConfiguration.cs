using E_learning.Core.Entities.Courses___content;
using E_learning.Core.Entities.LiveSessions;
using E_learning.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Repository.Config.LiveSessions
{
    public class LiveSessionConfiguration : IEntityTypeConfiguration<LiveSession>
    {
        public void Configure(EntityTypeBuilder<LiveSession> builder)
        {
            builder.ToTable("LiveSessions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(x => x.Description)
                   .HasMaxLength(2000);

            builder.Property(x => x.MeetingLink)
                   .HasMaxLength(500)
                   .IsRequired();

            builder.Property(x => x.RecordingUrl)
                   .HasMaxLength(500);

            builder.Property(x => x.DurationMinutes)
                   .HasDefaultValue(60)
                   .IsRequired();

            builder.Property(x => x.IsRecorded)
                   .HasDefaultValue(true);

            builder.Property(x => x.IsVisibleToStudents)
                   .HasDefaultValue(false);

            builder.Property(x => x.Status)
                   .IsRequired()
                   .HasDefaultValue(LiveSessionStatus.Scheduled);

            builder.Property(x => x.ScheduledAt)
                   .IsRequired();

            builder.HasOne(x => x.Instructor)
                .WithMany(i => i.LiveSessions)
                .HasForeignKey(x => x.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Course)
                   .WithMany(c => c.LiveSessions)
                   .HasForeignKey(x => x.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Attendees)
                   .WithOne(a => a.LiveSession)
                   .HasForeignKey(a => a.LiveSessionId);

            // Indexes
            builder.HasIndex(x => x.CourseId);
            builder.HasIndex(x => x.InstructorId);
            builder.HasIndex(x => x.ScheduledAt);
        }
    }
}
