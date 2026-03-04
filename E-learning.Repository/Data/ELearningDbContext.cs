using E_learning.Core.Entities.Base;
using E_learning.Core.Entities.Identity;
using E_learning.Repository.Interceptors;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;


namespace E_learning.Repository.Data
{
    public class ELearningDbContext
       : IdentityDbContext<ApplicationUser>
    {

        private readonly AuditInterceptor _auditInterceptor;

        public ELearningDbContext(
            DbContextOptions<ELearningDbContext> options,
            AuditInterceptor auditInterceptor)
            : base(options)
        {
            _auditInterceptor = auditInterceptor;
        }
        // Enrollment
        //public DbSet<Enrollment> Enrollments { get; set; }
        //public DbSet<LessonProgress> LessonProgress { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ELearningDbContext).Assembly);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(
                        entityType.ClrType, "e");

                    var property = Expression.Property(
                        parameter, nameof(ISoftDelete.IsDeleted));

                    var filter = Expression.Lambda(
                        Expression.Equal(
                            property,
                            Expression.Constant(false)),
                        parameter);

                    entityType.SetQueryFilter(filter);
                }
            }
        }

        // ─── OnConfiguring ───────────────────────
        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditInterceptor);
        }


    }
}

