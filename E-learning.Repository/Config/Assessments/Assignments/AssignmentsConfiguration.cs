using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_learning.Core.Entities.Assessments.Assignments;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace E_learning.Repository.Config.Assessments.Assignments
{
    public class AssignmentsConfiguration : IEntityTypeConfiguration<Assignment>
    {
        public void Configure(EntityTypeBuilder<Assignment> builder)
        {

            builder.ToTable("Assignments");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(a => a.Description)
                .HasMaxLength(int.MaxValue);

            builder.Property(a => a.DueDate)
                .IsRequired();

            builder.Property(a => a.TotalMarks)
                .IsRequired()
                .HasPrecision(7, 2);

            builder.Property(a => a.IsActive)
                .HasDefaultValue(true);

            builder.Property(a => a.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");


            builder.HasOne(a => a.Courses)
                .WithMany(c => c.Assignments)
                .HasForeignKey(a => a.CourseId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
