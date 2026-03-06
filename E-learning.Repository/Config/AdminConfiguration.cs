using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_learning.Core.Entities.Profiles;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Threading.Tasks;

namespace E_learning.Repository.Config
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.ToTable("Admins");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");
            builder.HasOne(a => a.User)
                   .WithOne(u => u.Admin)
                   .HasForeignKey<Admin>(a => a.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
