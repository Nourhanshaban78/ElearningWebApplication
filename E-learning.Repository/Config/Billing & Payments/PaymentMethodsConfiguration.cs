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
    public class PaymentMethodsConfiguration : IEntityTypeConfiguration<PaymentMethod>
    { 
           public void Configure(EntityTypeBuilder<PaymentMethod> builder)
            {
            builder.ToTable("PaymentMethods");

            // Primary Key
            builder.HasKey(p => p.Id);

            // Properties
            builder.Property(p => p.Type)
                   .IsRequired();

            builder.Property(p => p.CardLastFour)
                   .HasMaxLength(4);

            builder.Property(p => p.CardHolderName)
                   .HasMaxLength(200);

            builder.Property(p => p.PayPalEmail)
                   .HasMaxLength(200);

            builder.Property(p => p.IsDefault)
                   .HasDefaultValue(false);

            builder.Property(p => p.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Relationship: PaymentMethod -> User
            builder.HasOne(p => p.User)
                   .WithMany(u => u.PaymentMethods)
                   .HasForeignKey(p => p.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relationship: PaymentMethod -> Transactions
            builder.HasMany(p => p.PaymentTransactions)
                   .WithOne(t => t.PaymentMethod)
                   .HasForeignKey(t => t.PaymentMethodId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(p => p.UserId);
            builder.HasIndex(p => new { p.UserId, p.IsDefault });
        }
        }
    
    }