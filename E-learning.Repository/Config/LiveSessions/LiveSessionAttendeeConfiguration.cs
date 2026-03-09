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
    public class LiveSessionAttendeeConfiguration : IEntityTypeConfiguration<LiveSessionAttendee>
    {
        public void Configure(EntityTypeBuilder<LiveSessionAttendee> builder)
        {
            builder.ToTable("LiveSessionAttendees");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.JoinedAt)
                   .IsRequired();

            builder.Property(x => x.LeftAt);

            builder.Property(x => x.DurationSeconds);

            builder.HasOne(x => x.LiveSession)
                .WithMany(x => x.Attendees)
                .HasForeignKey(x => x.LiveSessionId);

            builder.HasOne(x => x.Student)
                   .WithMany(s => s.LiveSessionAttendees)
                   .HasForeignKey(x => x.StudentId);

            builder.HasIndex(x => new { x.LiveSessionId, x.StudentId })
                   .IsUnique(); // prevent duplicate attendance

            // Indexes
            builder.HasIndex(x => x.LiveSessionId);
            builder.HasIndex(x => x.StudentId);

            // Prevent same student joining the same session twice
            builder.HasIndex(x => new { x.LiveSessionId, x.StudentId })
                   .IsUnique();
        }
    }
    }
