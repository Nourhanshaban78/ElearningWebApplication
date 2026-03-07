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

            // Primary Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.IsSuperAdmin)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.Property(x => x.CreatedAt)
                   .IsRequired();

            // Relationship: Admin -> ApplicationUser (1:1)
            builder.HasOne(x => x.User)
                   .WithOne(x => x.Admin)
                   .HasForeignKey<Admin>(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relationship: Admin -> PayoutApprovals (1:M)
            builder.HasMany(x => x.PayoutApprovals)
                   .WithOne(x => x.Admin)
                   .HasForeignKey(x => x.AdminId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Index
            builder.HasIndex(x => x.UserId)
                   .IsUnique();
        }
    }
}
