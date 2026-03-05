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

            builder.HasKey(r => r.Id);

            builder.HasOne(r => r.Ticket)
                   .WithMany(t => t.Replies) 
                   .HasForeignKey(r => r.TicketId)
                   .OnDelete(DeleteBehavior.Cascade); 

            builder.HasOne(r => r.Sender)
                   .WithMany()  
                   .HasForeignKey(r => r.SenderId)
                   .OnDelete(DeleteBehavior.Restrict); 

            builder.Property(r => r.Body)
                   .IsRequired() 
                   .HasColumnType("NVARCHAR(MAX)");

            builder.Property(r => r.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");
 
        }
    }
}