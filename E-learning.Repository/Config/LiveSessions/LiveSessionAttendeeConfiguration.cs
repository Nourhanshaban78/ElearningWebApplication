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

            // ─── Primary Key ─────────────────────

            builder.HasKey(x => x.Id);

            // ─── Session Relation ────────────────

            builder.HasOne(x => x.Session)
                   .WithMany(x => x.Attendees)
                   .HasForeignKey(x => x.SessionId)
                   .OnDelete(DeleteBehavior.Cascade);

            // ─── Student Relation ────────────────

            //builder.HasOne(x => x.Student)
            //       .WithMany(x => x.LiveSessionAttendees)
            //       .HasForeignKey(x => x.StudentId)
            //       .OnDelete(DeleteBehavior.Restrict);

            // ─── Properties ──────────────────────

            builder.Property(x => x.JoinedAt)
                   .IsRequired();

            builder.Property(x => x.LeftAt)
                   .IsRequired(false);

            builder.Property(x => x.DurationSeconds)
                   .IsRequired(false);

            // ─── Prevent duplicate attendance ───

            builder.HasIndex(x => new { x.SessionId, x.StudentId })
                   .IsUnique();
        }
    }
    }
