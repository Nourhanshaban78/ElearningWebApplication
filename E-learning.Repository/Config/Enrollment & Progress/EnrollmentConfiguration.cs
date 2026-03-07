using E_learning.Core.Entities;
using E_learning.Core.Entities.Enrollment___Progress;
using E_learning.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Repository.Config
{

    public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Student)
                .WithMany(x => x.Enrollments)
                .HasForeignKey(x => x.StudentId);

            builder.HasOne(x => x.Course)
                .WithMany(x => x.Enrollments)
                .HasForeignKey(x => x.CourseId);

            builder.HasOne(x => x.Transaction)
                .WithMany()
                .HasForeignKey(x => x.TransactionId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    
}
}
