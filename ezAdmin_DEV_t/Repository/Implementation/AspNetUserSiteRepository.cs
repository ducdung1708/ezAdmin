using Models.DBContext;
using Models.EntityModels;
using Repository.Interfaces;

namespace Repository.Implementation
{
    public class AspNetUserSiteRepository : RepositoryBase<AspNetUserSite>, IAspNetUserSiteRepository
    {
        public AspNetUserSiteRepository(ezSQLDBContext dbContext) : base(dbContext)
        {
        }

        public bool UserInSite(string userID, Guid siteID)
        {
            return GetBy(s => s.UserId == userID && s.SiteId == siteID).Any();
        }

        public AspNetUserSite GetUserSiteDetail(string userID, Guid? siteID)
        {
            return GetBy(s => s.UserId == userID && (s.SiteId == siteID || !s.SiteId.HasValue)).FirstOrDefault();
        }
    }
}
