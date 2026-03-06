using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_learning.Core.Entities.AdminOperations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_learning.Repository.Config.AdminOperationsConfiguration
{
    public class SupportTicketsConfiguration : IEntityTypeConfiguration<SupportTickets>
    {
        public void Configure(EntityTypeBuilder<SupportTickets> builder)
        {
            builder.ToTable("SupportTickets");

            builder.HasKey(t => t.Id);

            builder.HasOne(t => t.User)
                   .WithMany()  
                   .HasForeignKey(t => t.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
                       
            builder.Property(t => t.Subject)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(t => t.Body)                  
                   .HasColumnType("NVARCHAR(MAX)")
                   .IsRequired();

            builder.Property(t => t.Type)
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(t => t.Status)
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .HasDefaultValue(Core.Enums.TicketStatus.Open);

            builder.HasOne(t => t.Assigned)
                   .WithMany()
                   .HasForeignKey(t => t.AssignedTo)
                   .OnDelete(DeleteBehavior.SetNull); 

            builder.Property(t => t.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(t => t.ResolvedAt)
                   .IsRequired(false);

            // Replies (One-to-Many)
            builder.HasMany(t => t.Replies)
                   .WithOne(r => r.Ticket)  
                   .HasForeignKey(r => r.TicketId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}