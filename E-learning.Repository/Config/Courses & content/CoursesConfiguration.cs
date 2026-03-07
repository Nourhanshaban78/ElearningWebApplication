using E_learning.Core.Entities.Courses___content;
using E_learning.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Repository.Config.Courses___content
{
    public class CourseConfiguration : IEntityTypeConfiguration<Courses>
    {
        public void Configure(EntityTypeBuilder<Courses> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Description)
                .IsRequired();

            builder.Property(x => x.Price)
                .HasColumnType("decimal(10,2)");

            builder.Property(x => x.Language)
                .HasMaxLength(10);

            builder.HasOne(x => x.Instructor)
                .WithMany(x => x.Courses)
                .HasForeignKey(x => x.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Level)
                .WithMany(x => x.Courses)
                .HasForeignKey(x => x.LevelId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.ApprovedBy)
                .WithMany(x => x.ApprovedCourses)
                .HasForeignKey(x => x.ApprovedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
