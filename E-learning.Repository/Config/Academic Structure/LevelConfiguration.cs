using E_learning.Core.Entities.Academic_Structure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Repository.Config.Academic_Structure
{
    public class LevelConfiguration : IEntityTypeConfiguration<Level>
    {
       
            public void Configure(EntityTypeBuilder<Level> builder)
            {
            builder.ToTable("Levels");

            // Primary Key
            builder.HasKey(l => l.Id);

            // Properties
            builder.Property(l => l.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(l => l.OrderIndex)
                   .IsRequired();

            builder.Property(l => l.IsActive)
                   .HasDefaultValue(true);

            builder.Property(l => l.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(l => l.CourseCount)
                   .HasDefaultValue(0);

            // Relationship with Stage
            builder.HasOne(l => l.Stage)
                   .WithMany(s => s.Levels)
                   .HasForeignKey(l => l.StageId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Relationship with Courses
            builder.HasMany(l => l.Courses)
                   .WithOne(c => c.Level)
                   .HasForeignKey(c => c.LevelId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Index (recommended)
            builder.HasIndex(l => new { l.StageId, l.OrderIndex })
                   .IsUnique();
        

    }
    }
}
