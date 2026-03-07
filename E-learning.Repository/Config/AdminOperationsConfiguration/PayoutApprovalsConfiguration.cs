using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_learning.Core.Entities.AdminOperations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_learning.Repository.Config.AdminOperationsConfiguration
{
    public class PayoutApprovalsConfiguration : IEntityTypeConfiguration<PayoutApprovals>
    {

        public void Configure(EntityTypeBuilder<PayoutApprovals> builder)
        {
            builder.ToTable("PayoutApprovals");

            // Primary Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.Decision)
                   .IsRequired();

            builder.Property(x => x.Notes)
                   .HasMaxLength(500);

            builder.Property(x => x.ProcessedAt)
                   .IsRequired();

            // Relationship with PayoutRequests
            builder.HasOne(x => x.PayoutRequest)
                   .WithMany() // change if PayoutRequests has collection
                   .HasForeignKey(x => x.PayoutRequestId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relationship with Admin
            builder.HasOne(x => x.Admin)
                   .WithMany() // change if Admin has collection
                   .HasForeignKey(x => x.AdminId)
                   .OnDelete(DeleteBehavior.Restrict);
        }



    
    }
}