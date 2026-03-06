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

            builder.HasKey(sr => sr.Id);

            builder.Property(sr => sr.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(sr => sr.ReminderTime)
                   .IsRequired();

            builder.Property(sr => sr.IsDaily)
                   .HasDefaultValue(true);

            builder.Property(sr => sr.IsActive)
                   .HasDefaultValue(true);

            builder.Property(sr => sr.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Relationships

            builder.HasOne(sr => sr.User)
                   .WithMany()
                   .HasForeignKey(sr => sr.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}