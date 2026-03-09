using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_learning.Core.Entities.Profiles;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Threading.Tasks;


namespace E_learning.Repository.Config.Profiles
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.ToTable("Admins");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.IsSuperAdmin)
                   .HasDefaultValue(false)
                   .IsRequired();

            builder.Property(x => x.CreatedAt)
                   .IsRequired();

            // ApplicationUser Relation (One-to-One)
            builder.HasOne(x => x.User)
                   .WithOne(u => u.Admin)
                   .HasForeignKey<Admin>(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Payout Approvals Relation
            builder.HasMany(x => x.PayoutApprovals)
                   .WithOne(p => p.Admin)
                   .HasForeignKey(p => p.AdminId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Index
            builder.HasIndex(x => x.UserId)
                   .IsUnique();
        
    }
    }
}
