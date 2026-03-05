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
    public class CoursesConfiguration : IEntityTypeConfiguration<Courses>
    {
        public void Configure(EntityTypeBuilder<Courses> builder)
        {
            builder.ToTable("Courses");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                   .ValueGeneratedNever();

            builder.HasOne(c => c.Instructor)
                   .WithMany(i => i.Courses)
                   .HasForeignKey(c => c.InstructorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Level)
                   .WithMany(l => l.Courses)
                   .HasForeignKey(c => c.LevelId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.Property(c => c.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(c => c.Description)
                   .IsRequired()
                   .HasColumnType("nvarchar(max)");

            builder.Property(c => c.WhatYouWillLearn)
                   .HasColumnType("nvarchar(max)");

            builder.Property(c => c.ThumbnailUrl)
                   .HasMaxLength(500);

            builder.Property(c => c.Language)
                   .HasMaxLength(10)
                   .HasDefaultValue("en");

            builder.Property(c => c.Price)
                   .HasColumnType("decimal(10,2)");

            builder.Property(c => c.Duration);

            builder.Property(c => c.Status)
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .HasDefaultValue(CoursesStatus.Draft);

            builder.Property(c => c.IsActive)
                   .HasDefaultValue(true);

            builder.Property(c => c.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(c => c.UpdatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(c => c.ApprovedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(c => c.ApprovedBy)
               .WithMany(u => u.ApprovedCourses)
               .HasForeignKey(c => c.ApprovedById)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
