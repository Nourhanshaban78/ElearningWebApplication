using E_learning.Core.Entities.Review_Certification_Schedule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_learning.Repository.Config
{
    public class ScheduleEventConfiguration : IEntityTypeConfiguration<ScheduleEvent>
    {
        public void Configure(EntityTypeBuilder<ScheduleEvent> builder)
        {
            builder.ToTable("ScheduleEvents");

            builder.HasKey(se => se.Id);

            builder.Property(se => se.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(se => se.Type)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(se => se.Priority)
                   .HasMaxLength(10)
                   .HasDefaultValue("Medium");

            builder.Property(se => se.StartTime)
                   .IsRequired();

            builder.Property(se => se.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Relationships

            builder.HasOne(se => se.User)
                   .WithMany()
                   .HasForeignKey(se => se.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(se => se.Course)
                   .WithMany()
                   .HasForeignKey(se => se.CourseId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}