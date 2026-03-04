
using E_learning.Core.Entities.Identity;
using E_learning.Repository.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_learning.API.Extensions
{
    public static class HostExtensions
    {
        public static async Task<IHost> MigrateDatabaseAsync(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILoggerFactory>()
                .CreateLogger("DatabaseMigration");

            try
            {
                logger.LogInformation("Starting migration...");

                var dbContext = services.GetRequiredService<ELearningDbContext>();

                // Apply Migrations
                await dbContext.Database.MigrateAsync();
                logger.LogInformation("Migration completed.");

              
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Migration failed.");
                throw;
            }

            return host;
        }
    }
}