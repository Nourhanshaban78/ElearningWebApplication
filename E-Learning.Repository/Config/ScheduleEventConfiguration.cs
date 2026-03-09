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
    public class ScheduleEventConfiguration
        : IEntityTypeConfiguration<ScheduleEvent>
    {
        public void Configure(EntityTypeBuilder<ScheduleEvent> builder)
        {
            builder.ToTable("ScheduleEvents");
            builder.HasKey(se => se.Id);

            builder.Property(se => se.Title)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(se => se.Type)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(se => se.Priority)
                   .HasMaxLength(10)
                   .HasDefaultValue("Medium");

            builder.Property(se => se.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Event → User
            builder.HasOne(se => se.User)
                   .WithMany()
                   .HasForeignKey(se => se.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Event → Course (Optional)
            builder.HasOne(se => se.Course)
                   .WithMany()
                   .HasForeignKey(se => se.CourseId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
