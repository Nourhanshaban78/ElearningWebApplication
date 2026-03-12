
using E_learning.API.Extensions;
using E_learning.Core.Entities.Identity;
using E_learning.Repository.Interceptors;
using E_Learning.Core.Base;
using E_Learning.Core.Interfaces.Repositories.Assessments.Assignments;
using E_Learning.Core.Interfaces.Repositories.Enrollments;
using E_Learning.Core.Interfaces.Services.Enrollments;
using E_Learning.Core.Repository;
using E_Learning.Repository.Data;
using E_Learning.Repository.Repositories;
using E_Learning.Repository.Repositories.GenericesRepositories.Assessments.Assignments;
using E_Learning.Repository.Repositories.GenericesRepositories.Enrollments;
using E_Learning.Service.Contract;
using E_Learning.Service.Contract.Assignments;
using E_Learning.Service.Mapping;
using E_Learning.Service.Services;
using E_Learning.Service.Services.AssignmentService;
using E_Learning.Service.Services.Enrollments;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace E_Learning.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Auto Mapper
            builder.Services.AddAutoMapper(typeof(EnrollmentMappingProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(AssignmentProfile).Assembly);

            // ResponseHandler
            builder.Services.AddTransient<ResponseHandler>();

            // Enrollment Services
            builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
            builder.Services.AddScoped<ILessonProgressService, LessonProgressService>();
            builder.Services.AddScoped<IAssignmentService,AssignmentService>();
            builder.Services.AddScoped<IAssignmentSubmissionService,AssignmentSubmissionService>();
            builder.Services.AddScoped<IFileService,FileService>();
            // Enrollment Repositories
            builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
            builder.Services.AddScoped<ILessonProgressRepository, LessonProgressRepository>();
            builder.Services.AddScoped<IAssignmentRepository, AssignmentRepository>();
            builder.Services.AddScoped<IAssignmentSubmissionRepository, AssignmentSubmissionRepository>();
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            // ─── Migration & Seeding ─────────────────────
            await app.MigrateDatabaseAsync();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}
