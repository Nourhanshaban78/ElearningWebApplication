using E_learning.Core.Entities.Courses___content;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Repository.Config.Courses___content
{
    internal class SectionsConfiguration : IEntityTypeConfiguration<Section>
    {
      
            public void Configure(EntityTypeBuilder<Section> builder)
            {
            builder.ToTable("Sections");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(x => x.OrderIndex)
                   .IsRequired();

            builder.Property(x => x.CreatedAt)
                   .IsRequired();

            // Course Relation
            builder.HasOne(x => x.Course)
                   .WithMany(c => c.Sections)
                   .HasForeignKey(x => x.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Lessons Relation
            builder.HasMany(x => x.Lessons)
                   .WithOne(l => l.Sections)
                   .HasForeignKey(l => l.SectionId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(x => x.CourseId);
            builder.HasIndex(x => new { x.CourseId, x.OrderIndex });
        }
        
    }
}
