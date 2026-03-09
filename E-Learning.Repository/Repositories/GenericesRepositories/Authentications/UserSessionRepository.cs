using E_Learning.core.Interfaces.Repositories.Authentications;
using E_Learning.Repository.Data;

namespace E_Learning.Repository.Repositories.GenericesRepositories.Authentications
{
    public class UserSessionRepository : IUserSessionRepository
    {
        public UserSessionRepository(ELearningDbContext context)
        {
            Context = context;
        }

        public ELearningDbContext Context { get; }
    }
}
