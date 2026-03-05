using E_learning.Core.Entities.Notifactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Repository.Config.NotificationsConfigurations
{
    public class NotificationsConfiguration
        : IEntityTypeConfiguration<Notifications>
    {
        public void Configure(EntityTypeBuilder<Notifications> builder)
        {
            builder.ToTable("Notifications");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(x => x.Body)
                   .HasMaxLength(1000)
                   .IsRequired();

            builder.Property(x => x.Type)
                   .HasConversion<string>()
                   .HasMaxLength(30)
                   .IsRequired();

            builder.Property(x => x.IsRead)
                   .HasDefaultValue(false);

            builder.Property(x => x.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Relatoinship

            builder.HasOne(x => x.User)
                   .WithMany(u => u.Notifications)
                   .HasForeignKey(x => x.User.Id)
                   .OnDelete(DeleteBehavior.Cascade);

            // Index    

            builder.HasIndex(x => new { x.User, x.IsRead});
            builder.HasIndex(x => x.CreatedAt);
                   

        }
    }
}
