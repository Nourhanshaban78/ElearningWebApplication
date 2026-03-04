using E_learning.Core.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Repository.Config
{
    public class UserSessionConfiguration : IEntityTypeConfiguration<UserSession>
    {
        public void Configure(EntityTypeBuilder<UserSession> builder)
        {
            builder.ToTable("UserSessions");

            builder.HasKey(us => us.Id);

            builder.HasIndex(us => us.RefreshToken)
                   .IsUnique()
                   .HasDatabaseName("IX_UserSessions_RefreshToken");

            builder.Property(us => us.RefreshToken)
                   .IsRequired();

            builder.Property(us => us.DeviceInfo)
                   .HasMaxLength(250);

            builder.Property(us => us.IsActive)
                   .IsRequired();

            builder.Property(us => us.ExpiresAt)
                   .IsRequired();

            builder.Property(us => us.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(us => us.User)
                   .WithMany(u => u.UserSessions)
                   .HasForeignKey(us => us.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
