using Models.DBContext;
using Models.EntityModels;
using Repository.Interfaces;

namespace Repository.Implementation
{
    public class AspNetUserSessionRepository : RepositoryBase<AspNetUserSession>, IAspNetUserSessionRepository
    {
        public AspNetUserSessionRepository(ezSQLDBContext dbContext) : base(dbContext)
        {
        }

        public List<AspNetUserSession> SessionsUserSites(string userID, Guid? siteID)
        {
            return GetBy(s => s.UserId == userID && s.SiteId == siteID).ToList();
        }
    }
}
