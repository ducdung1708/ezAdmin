using Models.EntityModels;

namespace Repository.Interfaces
{
    public interface IAspNetUserSessionRepository : IRepositoryBase<AspNetUserSession>
    {
        List<AspNetUserSession> SessionsUserSites(string userID, Guid? siteID);
    }
}
