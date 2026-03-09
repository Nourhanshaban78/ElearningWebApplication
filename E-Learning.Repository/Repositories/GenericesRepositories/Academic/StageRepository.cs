using E_Learning.Core.Interfaces.Repositories.Academic;
using E_Learning.Repository.Data;

namespace E_Learning.Repository.Repositories.GenericesRepositories.Academic
{
    internal class StageRepository :IStageRepository
    {
        public StageRepository(ELearningDbContext context)
        {
            _context = context;
        }

        public ELearningDbContext _context { get; }
    }
}
