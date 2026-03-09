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

            // Primary Key
            builder.HasKey(e => e.Id);

            // Properties
            builder.Property(e => e.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(e => e.Type)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(e => e.Priority)
                   .IsRequired()
                   .HasMaxLength(20)
                   .HasDefaultValue("Medium");

            builder.Property(e => e.StartTime)
                   .IsRequired();

            builder.Property(e => e.CreatedAt)
                   .IsRequired();

            // Relationships

            builder.HasOne(e => e.Instructor)
                   .WithMany() // change if Instructor has ICollection<ScheduleEvent>
                   .HasForeignKey(e => e.InstructorId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Course)
                   .WithMany() // change if Course has ICollection<ScheduleEvent>
                   .HasForeignKey(e => e.CourseId)
                   .OnDelete(DeleteBehavior.SetNull);

            // Indexes (recommended)
            builder.HasIndex(e => e.InstructorId);
            builder.HasIndex(e => e.StartTime);
        }
    }
}