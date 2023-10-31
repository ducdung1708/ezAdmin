using Models.EntityModels;
using Models.Models.Response;
using Models.Models.Result;

namespace Repository.Interfaces
{
    public interface IAspNetGroupUserRepository : IRepositoryBase<AspNetGroupUser>
    {
        /// <summary>
        /// Danh sách nhóm theo Site
        /// </summary>
        /// <param name="SiteID"></param>
        /// <returns></returns>
        List<GroupUserInfoResult> GetGroupUsersSite(Guid? siteID);

        /// <summary>
        /// Thông tin chi tiết nhóm User Template
        /// </summary>
        /// <param name="groupUserCode"></param>
        /// <returns></returns>
        AspNetGroupUser GetGroupUserTemplateDetail(string groupUserCode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="siteID"></param>
        /// <returns></returns>
        List<GroupUserResourceResult> GetGroupUserResourseBySite(Guid? siteID);

    }
}
