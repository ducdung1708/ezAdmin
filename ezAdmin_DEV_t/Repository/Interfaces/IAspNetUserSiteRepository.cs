using Models.EntityModels;

namespace Repository.Interfaces
{
    public interface IAspNetUserSiteRepository: IRepositoryBase<AspNetUserSite>
    {
        /// <summary>
        /// Kiểm tra User thuộc Site
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="siteID"></param>
        /// <returns></returns>
        bool UserInSite(string userID, Guid siteID);

        /// <summary>
        /// Chi tiết 1 UserSite
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="siteID"></param>
        /// <returns></returns>
        AspNetUserSite GetUserSiteDetail(string userID, Guid? siteID);
    }
}
