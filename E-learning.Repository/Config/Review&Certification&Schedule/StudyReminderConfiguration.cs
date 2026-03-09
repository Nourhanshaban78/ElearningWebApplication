using E_learning.Core.Entities.Review_Certification_Schedule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_learning.Repository.Config
{
    public class StudyReminderConfiguration : IEntityTypeConfiguration<StudyReminder>
    {
        public void Configure(EntityTypeBuilder<StudyReminder> builder)
        {
            builder.ToTable("StudyReminders");

            // Primary Key
            builder.HasKey(r => r.Id);

            // Properties
            builder.Property(r => r.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(r => r.ReminderTime)
                   .IsRequired();

            builder.Property(r => r.IsDaily)
                   .HasDefaultValue(true);

            builder.Property(r => r.IsActive)
                   .HasDefaultValue(true);

            builder.Property(r => r.CreatedAt)
                   .IsRequired();

            // Relationship
            builder.HasOne(r => r.User)
                   .WithMany() // change if ApplicationUser has ICollection<StudyReminder>
                   .HasForeignKey(r => r.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Index (recommended for queries)
            builder.HasIndex(r => r.UserId);
        }
    }
}