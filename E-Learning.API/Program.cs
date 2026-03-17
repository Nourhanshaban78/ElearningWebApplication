<<<<<<< Updated upstream
<<<<<<< Updated upstream
﻿
using E_learning.API.Extensions;
using E_learning.Core.Entities.Identity;
using E_learning.Repository.Interceptors;
using E_Learning.Core.Interfaces.Repositories;
using E_Learning.Core.Base;
using E_Learning.Core.Interfaces.Repositories.Enrollments;
using E_Learning.Core.Interfaces.Repositories.LiveSessions;
using E_Learning.Core.Interfaces.Repositories.Profile;
using E_Learning.Core.Interfaces.Services.Academic;
using E_Learning.Core.Interfaces.Services.Courses;
using E_Learning.Core.Interfaces.Services.Enrollments;
using E_Learning.Core.Repository;
using E_Learning.Repository.Data;
using E_Learning.Repository.Repositories;
using E_Learning.Repository.Repositories.GenericesRepositories.Enrollments;
using E_Learning.Service.Contract.Notifications;
using E_Learning.Repository.Repositories.GenericesRepositories.LiveSessions;
using E_Learning.Repository.Repositories.GenericesRepositories.Profile;
using E_Learning.Service.Contract;
using E_Learning.Service.Contract.Assignments;
using E_Learning.Service.Mapping;
using E_Learning.Service.Services;
using E_Learning.Service.Services.Academic;
using E_Learning.Service.Services.Academic.Stage;
using E_Learning.Service.Services.AssignmentService;
using E_Learning.Service.Services.Courses;
using E_Learning.Service.Services.Enrollments;
using E_Learning.Service.Services.Notifications;
using E_Learning.Service.Services.LiveSessionServices;
using E_Learning.Service.Services.Profiles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using E_Learning.Repository.Repositories.GenericesRepositories;
using E_Learning.API.Extensions;

namespace E_Learning.API
=======
﻿namespace E_Learning.API
>>>>>>> Stashed changes
=======
﻿namespace E_Learning.API
>>>>>>> Stashed changes
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
            builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));

            builder.Services.AddTransient<ResponseHandler>();
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<IInstructorService, InstructorService>();
            builder.Services.AddScoped<IStudentService, StudentService>();


            builder.Services.AddScoped<ILiveSessionService, LiveSessionService>();
            builder.Services.AddScoped<ILiveSessionAttendeeService, LiveSessionAttendeeService>();

            builder.Services.AddScoped<ILiveSessionRepository, LiveSessionRepository>();
            builder.Services.AddScoped<ILiveSessionAttendeeRepository, LiveSessionAttendeeRepository>();
            builder.Services.AddScoped<IAdminProfileRepository, AdminProfileRepository>();
            builder.Services.AddScoped<IInstructorProfileRepository, InstructorProfileRepository>();
            builder.Services.AddScoped<IStudentProfileRepository, StudentProfileRepository>();
<<<<<<< Updated upstream

=======
            builder.Services.AddScoped<ICertificateServices, CertificateServices>();
            builder.Services.AddScoped<IExamAttemptServices, ExamAttemptServices>();
            builder.Services.AddScoped<IExamServices,ExamServices>();
            builder.Services.AddScoped<IExamQuestionServices,ExamQuestionServices>();
            builder.Services.AddScoped<IExamAttemptAnswerServices,ExamAttemptAnswerServices>();
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes


            //// Auto Mapper
            builder.Services.AddAutoMapper(typeof(EnrollmentMappingProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(AssignmentProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(LiveSessionMappingProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(AdminProfileMapping).Assembly);
            builder.Services.AddAutoMapper(typeof(AcademicMappingProfile).Assembly);

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            // builder.Services.AddAutoMapper(typeof(LiveSessionMappingProfile));
            // ResponseHandler
            builder.Services.AddTransient<ResponseHandler>();
            builder.Services.AddApplicationServices();


            // AddApplicationServices
            builder.Services.AddApplicationServices(builder.Configuration);
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            //Add fake data
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<ELearningDbContext>();
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                await DbSeeder.SeedAsync(context, userManager);
            }
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


            app.MapControllers();

            app.Run();
        }
    }
}