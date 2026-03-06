using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using E_learning.Core.Entities.Profiles;
using E_learning.Core.Entities.Identity;

using System.Threading.Tasks;

namespace E_learning.Repository.Config
{
    public class InstructorConfigration : IEntityTypeConfiguration<Instructor>

    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.ToTable("Instructors");
            builder.HasKey(i => i.Id);
            builder.HasOne(i => i.User)
                   .WithOne(u=>u.Instructor)
                   .HasForeignKey<Instructor>(i => i.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.Property(i => i.Headline)
                   .HasMaxLength(255);
            builder.Property(i => i.About)
                   .HasMaxLength(2000);
            builder.Property(i => i.TotalEarnings)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0);
            builder.Property(i => i.PendingPayout)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0);
            builder.Property(i => i.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()")
                   .IsRequired();
            builder.Property(i => i.UpdatedAt)
                   .HasDefaultValueSql("GETUTCDATE()")
                   .IsRequired();
            builder.HasMany(i => i.Courses)
                   .WithOne(c => c.Instructor)
                   .HasForeignKey(c => c.InstructorId)
                   .OnDelete(DeleteBehavior.Cascade);






        }
    }
}
