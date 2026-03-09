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
    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.ToTable("Lessons");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(x => x.Type)
                   .IsRequired();

            builder.Property(x => x.VideoUrl)
                   .HasMaxLength(500);

            builder.Property(x => x.Content)
                   .HasColumnType("nvarchar(max)");

            builder.Property(x => x.DurationSeconds)
                   .IsRequired();

            builder.Property(x => x.OrderIndex)
                   .IsRequired();

            builder.Property(x => x.IsFreePreview)
                   .HasDefaultValue(false);

            builder.Property(x => x.CreatedAt)
                   .IsRequired();

            // Section Relation
            builder.HasOne(x => x.Sections)
                   .WithMany(s => s.Lessons)
                   .HasForeignKey(x => x.SectionId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Lesson Quizzes
            builder.HasMany(x => x.Quizzes)
                   .WithOne(q => q.Lesson)
                   .HasForeignKey(q => q.LessonId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Indexes (recommended)
            builder.HasIndex(x => x.SectionId);
            builder.HasIndex(x => new { x.SectionId, x.OrderIndex });
        }
    }
}
