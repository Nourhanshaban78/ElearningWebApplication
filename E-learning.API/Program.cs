
using E_learning.API.Extensions;
using E_learning.Core.Entities.Identity;
using E_learning.Repository.Data;
using E_learning.Repository.Interceptors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace E_learning.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // ─── DbContext ───────────────────────────────


            // For Auditing Interceptor
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<AuditInterceptor>();

            // DbContext Default
            builder.Services.AddDbContext<ELearningDbContext>((serviceProvider, options) =>
            {
                var interceptor = serviceProvider.GetRequiredService<AuditInterceptor>();

                var connectionString = builder.Environment.IsDevelopment()
                    ? builder.Configuration.GetConnectionString("DefaultConnection")
                    : builder.Configuration.GetConnectionString("DeployConnection");

                options.UseSqlServer(connectionString)
                       .AddInterceptors(interceptor);
            });


            builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ELearningDbContext>().AddDefaultTokenProviders();


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // ─── Migration & Seeding ─────────────────────
          //  await app.MigrateDatabaseAsync();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
