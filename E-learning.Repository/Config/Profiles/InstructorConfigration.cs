using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using E_learning.Core.Entities.Profiles;
using E_learning.Core.Entities.Identity;

using System.Threading.Tasks;

namespace E_learning.Repository.Config.Profiles
{
    public class InstructorConfigration : IEntityTypeConfiguration<Instructor>

    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.ToTable("Instructors");

            // Primary Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.Headline)
                   .HasMaxLength(200);

            builder.Property(x => x.About)
                   .HasMaxLength(2000);

            builder.Property(x => x.TotalEarnings)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0);

            builder.Property(x => x.PendingPayout)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0);

            builder.Property(x => x.CreatedAt)
                   .IsRequired();

            builder.Property(x => x.UpdatedAt)
                   .IsRequired();

            // ─── Relationship: Instructor ↔ ApplicationUser (1:1)
            builder.HasOne(x => x.User)
                   .WithOne(x => x.Instructor)
                   .HasForeignKey<Instructor>(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // ─── Relationship: Instructor → Courses (1:M)
            builder.HasMany(x => x.Courses)
                   .WithOne(x => x.Instructor)
                   .HasForeignKey(x => x.InstructorId)
                   .OnDelete(DeleteBehavior.Restrict);

            // ─── Relationship: Instructor → InstructorEarnings (1:M)
            builder.HasMany(x => x.InstructorEarnings)
                   .WithOne(x => x.Instructor)
                   .HasForeignKey(x => x.InstructorId)
                   .OnDelete(DeleteBehavior.Restrict);

            // ─── Relationship: Instructor → LiveSessions (1:M)
            builder.HasMany(x => x.LiveSessions)
                   .WithOne()
                   .HasForeignKey("InstructorId")
                   .OnDelete(DeleteBehavior.Cascade);

            // Index
            builder.HasIndex(x => x.UserId).IsUnique();
        }
    }
}
