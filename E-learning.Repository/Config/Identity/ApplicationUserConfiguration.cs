using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Repository.Config.Identity
{
    using E_learning.Core.Entities.Identity;
    using E_learning.Core.Entities.Notifactions;
    using E_learning.Core.Entities.Profiles;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("Users");

            builder.Property(x => x.FullName)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(x => x.Bio)
                   .HasMaxLength(1000);

            builder.Property(x => x.ProfileImage)
                   .HasMaxLength(500);

            builder.Property(x => x.Location)
                   .HasMaxLength(200);

            builder.Property(x => x.DateOfBirth);

            builder.Property(x => x.IsActive)
                   .IsRequired();

            builder.Property(x => x.MemberSince)
                   .IsRequired();

            builder.Property(x => x.UpdatedAt)
                   .IsRequired();

            // Notification Settings (One-to-One)
            builder.HasOne(x => x.NotificationSettings)
                   .WithOne(n => n.User)
                   .HasForeignKey<NotificationSetting>(n => n.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Notifications (One-to-Many)
            builder.HasMany(x => x.Notifications)
                   .WithOne(n => n.User)
                   .HasForeignKey(n => n.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Payment Methods
            builder.HasMany(x => x.PaymentMethods)
                   .WithOne(p => p.User)
                   .HasForeignKey(p => p.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // User Sessions
            builder.HasMany(x => x.UserSessions)
                   .WithOne(s => s.User)
                   .HasForeignKey(s => s.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // OTP Codes
            builder.HasMany(x => x.OtpCodes)
                   .WithOne(o => o.User)
                   .HasForeignKey(o => o.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Profiles (One-to-One)
            builder.HasOne(x => x.Student)
                   .WithOne(s => s.User)
                   .HasForeignKey<Student>(s => s.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Instructor)
                   .WithOne(i => i.User)
                   .HasForeignKey<Instructor>(i => i.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Admin)
                   .WithOne(a => a.User)
                   .HasForeignKey<Admin>(a => a.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Useful indexes
            builder.HasIndex(x => x.MemberSince);
            builder.HasIndex(x => x.IsActive);
        }
    }
}
