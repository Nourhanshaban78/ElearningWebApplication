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
    public class LessonConfiguration : IEntityTypeConfiguration<Lessons>
    {
        public void Configure(EntityTypeBuilder<Lessons> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasOne(x => x.Sections)
                .WithMany(x => x.Lessons)
                .HasForeignKey(x => x.SectionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
