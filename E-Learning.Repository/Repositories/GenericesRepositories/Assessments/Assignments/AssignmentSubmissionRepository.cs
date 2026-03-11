using E_Learning.Core.Entities.Assessments.Assignments;
using E_Learning.Core.Interfaces.Repositories.Assessments.Assignments;
using E_Learning.Repository.Data;

namespace E_Learning.Repository.Repositories.GenericesRepositories.Assessments.Assignments
{
    public class AssignmentSubmissionRepository : GenericRepository<AssignmentSubmission, int>,IAssignmentSubmissionRepository
    {
        public AssignmentSubmissionRepository(ELearningDbContext context) : base(context)
        {
            _context = context;
        }
       

        public ELearningDbContext _context { get; }
    }
}
