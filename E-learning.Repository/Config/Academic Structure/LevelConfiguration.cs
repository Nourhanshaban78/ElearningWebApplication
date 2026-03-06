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
            builder.ToTable("Levels");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(l => l.OrderIndex)
                   .IsRequired();

            builder.Property(l => l.IsActive)
                   .HasDefaultValue(true);

            builder.Property(l => l.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(l => l.Stage)
                   .WithMany(s => s.Levels)
                   .HasForeignKey(l => l.StageId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
