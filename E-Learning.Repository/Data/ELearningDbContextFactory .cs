using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace E_Learning.Repository.Data
{
    public class ELearningDbContextFactory : IDesignTimeDbContextFactory<ELearningDbContext>
    {
        public ELearningDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ELearningDbContext>();

            optionsBuilder.UseSqlServer(
                    "Server=db46311.public.databaseasp.net; Database=db46311; User Id=db46311; Password=5w?Am=K4Md7!; Encrypt=False; MultipleActiveResultSets=True;");

            return new ELearningDbContext(optionsBuilder.Options, null);
        }
    }
}
