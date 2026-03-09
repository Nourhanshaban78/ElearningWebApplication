using E_Learning.Core.Interfaces.Repositories.AdminOperations;
using E_Learning.Repository.Data;

namespace E_Learning.Repository.Repositories.GenericesRepositories.AdminOperations
{
    public class CourseAnalyticsSnapshotRepository :ICourseAnalyticsSnapshotRepository
    {
        public CourseAnalyticsSnapshotRepository(ELearningDbContext context)
        {
            _context = context;
        }

        public ELearningDbContext _context { get; }
    }
}
