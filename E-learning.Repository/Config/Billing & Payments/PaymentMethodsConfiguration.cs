using E_learning.Core.Entities.Billing___Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Repository.Config.Billing___Payments
{
    public class PaymentMethodsConfiguration : IEntityTypeConfiguration<PaymentMethods>
    { 
           public void Configure(EntityTypeBuilder<PaymentMethods> builder)
            {
                builder.ToTable("PaymentMethods");

                // Primary Key
                builder.HasKey(p => p.Id);

                // Properties
                builder.Property(p => p.Type)
                    .HasConversion<int>()
                    .IsRequired();

                builder.Property(p => p.CardLastFour)
                    .HasMaxLength(4);

                builder.Property(p => p.CardHolderName)
                    .HasMaxLength(150);

                builder.Property(p => p.PayPalEmail)
                    .HasMaxLength(256);

                builder.Property(p => p.IsDefault)
                    .HasDefaultValue(false);

                builder.Property(p => p.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");

                // Relationships

                // User → PaymentMethods (1 : many)
                builder.HasOne(p => p.User)
                    .WithMany(u => u.PaymentMethods)
                    .HasForeignKey(p => p.UserId)
                    .OnDelete(DeleteBehavior.Restrict); // ❌ لا Cascade (يمنع Multiple Cascade Paths)

                // PaymentMethods → PaymentTransactions (1 : many)
                builder.HasMany(p => p.PaymentTransactions)
                    .WithOne(t => t.PaymentMethods)
                    .HasForeignKey(t => t.PaymentMethodId)
                    .OnDelete(DeleteBehavior.Restrict); // ❌ لا Cascade

                // Index (recommended)
                builder.HasIndex(p => new { p.UserId, p.IsDefault });
            }
        }
    
    }