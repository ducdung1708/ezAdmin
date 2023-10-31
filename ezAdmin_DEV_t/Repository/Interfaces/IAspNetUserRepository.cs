using Models.EntityModels;
using Models.Models.Response;
using Models.Models.Result;

namespace Repository.Interfaces
{
    public interface IAspNetUserRepository : IRepositoryBase<AspNetUser>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="siteID"></param>
        /// <returns></returns>
        List<AccountGetListResponse> GetUserBySite(Guid? siteID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="siteID"></param>
        /// <returns></returns>
        List<MenuGroupUserSiteResourceResult> GetUserMenusRolesResource(string userID, Guid? siteID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="siteID"></param>
        /// <returns></returns>
        UserSiteRoleNameResult? GetUserSiteRoleName(string userID, Guid? siteID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="siteID"></param>
        /// <returns></returns>
        List<UserResourceResult> GetUserResourseBySite(Guid? siteID);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<UserSystemResult> GetSystemUser();
    }
}
