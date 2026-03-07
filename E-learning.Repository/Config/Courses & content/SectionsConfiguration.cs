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
                builder.HasKey(x => x.Id);

                builder.Property(x => x.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                builder.HasOne(x => x.Courses)
                    .WithMany(x => x.Sections)
                    .HasForeignKey(x => x.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);
            }
        
    }
}
