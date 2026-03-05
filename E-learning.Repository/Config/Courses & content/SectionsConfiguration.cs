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
    internal class SectionsConfiguration : IEntityTypeConfiguration<Sections>
    {
        public void Configure(EntityTypeBuilder<Sections> builder)
        {
            builder.ToTable("Sections");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                   .ValueGeneratedNever();

            builder.HasOne(s => s.Courses)
                   .WithMany(c => c.Sections)
                   .HasForeignKey(s => s.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(s => s.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(s => s.OrderIndex)
                   .IsRequired();

            builder.Property(s => s.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
