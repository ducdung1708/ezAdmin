using Infrastructure.ConstantsDefine.HardCode;
using Models.DBContext;
using Models.EntityModels;
using Models.Models.Result;
using Repository.Interfaces;

namespace Repository.Implementation
{
    public class AspNetGroupUserRepository : RepositoryBase<AspNetGroupUser>, IAspNetGroupUserRepository
    {
        private readonly ezSQLDBContext _dbcontext;
        public AspNetGroupUserRepository(ezSQLDBContext dbContext) : base(dbContext)
        {
            _dbcontext = dbContext;
        }

        public List<GroupUserResourceResult> GetGroupUserResourseBySite(Guid? siteID)
        {
            return _dbcontext.AspNetGroupUsers
                .Where(s => s.SiteId == siteID)
                .Select(s => new GroupUserResourceResult
                {
                    GroupUserId = s.AspNetGroupUserId,
                    GroupUserCode = s.GroupUserCode,
                    GroupUserKeyword = s.Keyword
                })
                .ToList();
        }

        public List<GroupUserInfoResult> GetGroupUsersSite(Guid? siteID)
        {
            return _dbcontext.AspNetGroupUsers
                .Where(s => !(s.IsSystem ?? false) && ((!siteID.HasValue && !s.SiteId.HasValue) || s.SiteId == siteID))
                .Select(s => new GroupUserInfoResult
                {
                    AspNetGroupUserID = s.AspNetGroupUserId,
                    GroupUserCode = s.GroupUserCode,
                    GroupUserKeyword = s.Keyword,
                    GroupUserDescription = s.GroupUserDescription,
                    GroupLevel = s.RoleLevel,
                    SiteId = s.SiteId,
                    CreatedDate = s.CreatedDate.HasValue ? s.CreatedDate.Value.AddMinutes(420) : null,
                    CreatedByUserId = s.CreatedByUserId
                })
                .ToList();
        }

        public AspNetGroupUser GetGroupUserTemplateDetail(string groupUserCode)
        {
            return _dbcontext.AspNetGroupUsers.FirstOrDefault(s => !(s.IsSystem ?? false) && !s.SiteId.HasValue && s.GroupUserCode == groupUserCode);
        }
    }
}
