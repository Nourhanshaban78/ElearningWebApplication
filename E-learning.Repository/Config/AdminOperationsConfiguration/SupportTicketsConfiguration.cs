using E_learning.Core.Entities.AdminOperations;
using E_learning.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_learning.Repository.Config.AdminOperationsConfiguration
{
    public class SupportTicketsConfiguration : IEntityTypeConfiguration<SupportTickets>
    {
        public void Configure(EntityTypeBuilder<SupportTickets> builder)
        {
            builder.ToTable("SupportTickets");

            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Subject)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(t => t.Body)
                   .IsRequired()
                   .HasMaxLength(4000);

            builder.Property(t => t.Type)
                   .IsRequired();

            builder.Property(t => t.Status)
                   .HasDefaultValue(TicketStatus.Open);

            builder.Property(t => t.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Relationship: Ticket -> User (creator)
            builder.HasOne(t => t.User)
                   .WithMany()
                   .HasForeignKey(t => t.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Relationship: Ticket -> Assigned Admin/User
            builder.HasOne(t => t.Assigned)
                   .WithMany()
                   .HasForeignKey(t => t.AssignedTo)
                   .OnDelete(DeleteBehavior.SetNull);

            // Relationship: Ticket -> Replies
            builder.HasMany(t => t.Replies)
                   .WithOne(r => r.Ticket)
                   .HasForeignKey(r => r.TicketId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(t => t.UserId);
            builder.HasIndex(t => t.AssignedTo);
            builder.HasIndex(t => t.Status);
        }
    }
}