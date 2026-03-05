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
    public class LessonsConfiguration : IEntityTypeConfiguration<Lessons>
    {
        public void Configure(EntityTypeBuilder<Lessons> builder)
        {
            builder.ToTable("Lessons");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Id)
                   .ValueGeneratedNever();

            builder.HasOne(l => l.Sections)
                   .WithMany(s => s.Lessons)
                   .HasForeignKey(l => l.SectionId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(l => l.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(l => l.Type)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(l => l.VideoUrl)
                   .HasMaxLength(500);

            builder.Property(l => l.Content)
                   .HasColumnType("nvarchar(max)");

            builder.Property(l => l.DurationSeconds);

            builder.Property(l => l.OrderIndex)
                   .IsRequired();

            builder.Property(l => l.IsFreePreview)
                   .HasDefaultValue(false);

            builder.Property(l => l.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
