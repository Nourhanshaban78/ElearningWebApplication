using E_learning.Core.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Repository.Config.Identity
{
    public class OtpCodesConfiguration
     : IEntityTypeConfiguration<OtpCodes>
    {
        public void Configure(EntityTypeBuilder<OtpCodes> builder)
        {
            builder.ToTable("OtpCodes");

            builder.HasKey(otp => otp.Id);

            builder.HasIndex(otp => otp.Code)
                   .HasDatabaseName("IX_OtpCodes_Code");

            builder.Property(otp => otp.Code)
                   .HasMaxLength(10)
                   .IsRequired();

            builder.Property(otp => otp.Purpose)
                   .HasMaxLength(30)
                   .IsRequired();

            builder.Property(otp => otp.IsUsed)
                   .HasDefaultValue(false);

            builder.Property(otp => otp.ExpiresAt)
                   .IsRequired();

            builder.Property(otp => otp.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()")
                   .IsRequired();

            builder.HasOne(otp => otp.User)
                   .WithMany(u => u.OtpCodes)
                   .HasForeignKey(otp => otp.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
