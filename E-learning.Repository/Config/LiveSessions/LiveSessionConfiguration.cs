using E_learning.Core.Entities.Courses___content;
using E_learning.Core.Entities.LiveSessions;
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
            // Primary Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Description)
                .HasMaxLength(1000);

            builder.Property(x => x.MeetingLink)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.RecordingUrl)
                .HasMaxLength(500);

            builder.Property(x => x.DurationMinutes)
                .IsRequired();

            builder.Property(x => x.ScheduledAt)
                .IsRequired();

            // Enum Conversion (recommended)
            builder.Property(x => x.Status)
                .HasConversion<string>()
                .IsRequired();

            // Ignore Computed Property
            builder.Ignore(x => x.EndAt);

            // Instructor Relationship
            builder.HasOne(x => x.Instructor)
                .WithMany(x => x.LiveSessions)
                .HasForeignKey(x => x.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Course Relationship
            builder.HasOne<Courses>()
                .WithMany()
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes (performance)
            builder.HasIndex(x => x.InstructorId);
            builder.HasIndex(x => x.CourseId);
            builder.HasIndex(x => x.ScheduledAt);
        }
    }
}
