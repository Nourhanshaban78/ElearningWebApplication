using E_learning.Core.Entities.Academic_Structure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Repository.Config.Academic_Structure
{
    public class LevelConfiguration : IEntityTypeConfiguration<Level>
    {
       
            public void Configure(EntityTypeBuilder<Level> builder)
            {
                builder.HasKey(x => x.Id);

                builder.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                builder.Property(x => x.OrderIndex)
                    .IsRequired();

                builder.Ignore(x => x.CourseCount);

                builder.HasOne(x => x.Stage)
                    .WithMany(x => x.Levels)
                    .HasForeignKey(x => x.StageId)
                    .OnDelete(DeleteBehavior.Restrict);

                builder.HasMany(x => x.Courses)
                    .WithOne(x => x.Level)
                    .HasForeignKey(x => x.LevelId)
                    .OnDelete(DeleteBehavior.SetNull);
            
        }
    }
}
