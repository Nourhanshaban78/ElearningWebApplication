using E_learning.Core.Entities.Notifactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Repository.Config.NotificationsConfigurations
{
    public class NotificationsSettingsConfiguration : IEntityTypeConfiguration<NotificationSettings>
    {
        public void Configure(EntityTypeBuilder<NotificationSettings> builder)
        {
            builder.ToTable("NotificationSettings");

            builder.HasKey(x => x.Id);

            // Relationship One-One

            builder.HasIndex(x => x.UserId).IsUnique();

            builder.Property(x => x.CourseAnnouncement)
                   .HasDefaultValue(true);

            builder.Property(x => x.AssignmentReminder)
                   .HasDefaultValue(true);

            builder.Property(x => x.ExamNotification)
                   .HasDefaultValue(true);

            builder.Property(x => x.PlatformUpdates)
                   .HasDefaultValue(true);

            builder.Property(x => x.InAppNotification)
                   .HasDefaultValue(true);

            builder.Property(x => x.EmailNotification)
                   .HasDefaultValue(true);

            builder.HasOne(x => x.User)
                .WithOne(u => u.NotificationSettings)
                .HasForeignKey<NotificationSettings>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

