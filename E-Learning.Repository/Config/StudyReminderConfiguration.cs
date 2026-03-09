using E_Learning.Core.Entities.Schedule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Repository.Config
{
    public class StudyReminderConfiguration
        : IEntityTypeConfiguration<StudyReminder>
    {
        public void Configure(EntityTypeBuilder<StudyReminder> builder)
        {
            builder.ToTable("StudyReminders");
            builder.HasKey(sr => sr.Id);

            builder.Property(sr => sr.Title)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(sr => sr.IsDaily)
                   .HasDefaultValue(true);

            builder.Property(sr => sr.IsActive)
                   .HasDefaultValue(true);

            builder.Property(sr => sr.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Reminder → User
            builder.HasOne(sr => sr.User)
                   .WithMany()
                   .HasForeignKey(sr => sr.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
