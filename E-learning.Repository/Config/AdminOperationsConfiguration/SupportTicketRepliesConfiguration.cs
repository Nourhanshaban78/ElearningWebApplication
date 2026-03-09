using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_learning.Core.Entities.AdminOperations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_learning.Repository.Config.AdminOperationsConfiguration
{
    public class SupportTicketRepliesConfiguration : IEntityTypeConfiguration<SupportTicketReplies>
    {
        public void Configure(EntityTypeBuilder<SupportTicketReplies> builder)
        {
            builder.ToTable("SupportTicketReplies");

            // Primary Key
            builder.HasKey(r => r.Id);

            // Properties
            builder.Property(r => r.Body)
                   .IsRequired()
                   .HasMaxLength(2000);

            builder.Property(r => r.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Relationship: Reply -> Ticket
            builder.HasOne(r => r.Ticket)
                   .WithMany(t => t.Replies)
                   .HasForeignKey(r => r.TicketId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relationship: Reply -> Sender (ApplicationUser)
            builder.HasOne(r => r.Sender)
                   .WithMany()
                   .HasForeignKey(r => r.SenderId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Self reference for nested replies
            builder.HasMany(r => r.Replies)
                   .WithOne()
                   .HasForeignKey("ParentReplyId")
                   .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(r => r.TicketId);
            builder.HasIndex(r => r.SenderId);
        }
    }
}