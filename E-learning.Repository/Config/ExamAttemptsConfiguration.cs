using E_learning.Core.Entities.Assessments.Exams;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Repository.Config
{
    public class ExamAttemptsConfiguration : IEntityTypeConfiguration<ExamAttempts>
    {
        public void Configure(EntityTypeBuilder<ExamAttempts> builder)
        {
            //builder.HasOne(e => e.Student)
            //       .WithMany(u => u.ExamAttempts)
            //       .HasForeignKey(e => e.StudentId)
            //       .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
