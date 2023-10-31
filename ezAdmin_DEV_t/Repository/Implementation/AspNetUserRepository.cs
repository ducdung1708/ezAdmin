using Infrastructure.ConstantsDefine.HardCode;
using Models.DBContext;
using Models.EntityModels;
using Models.Extension;
using Models.Models.Response;
using Models.Models.Result;
using Repository.Interfaces;

namespace Repository.Implementation
{
    public class AspNetUserRepository : RepositoryBase<AspNetUser>, IAspNetUserRepository
    {
        private readonly ezSQLDBContext _dbcontext;
        public AspNetUserRepository(ezSQLDBContext dbContext) : base(dbContext)
        {
            _dbcontext = dbContext;
        }

        public List<UserSystemResult> GetSystemUser()
        {
            return _dbcontext.AspNetUserSites.Where(s => s.SiteId == null)
                .Join(_dbcontext.AspNetUsers, us => us.UserId, u => u.Id, (us, u) => new {us, u})
                .Select(s => new UserSystemResult
                {
                    UserId = s.u.Id,
                    UserName = s.u.UserName,
                    Email = s.u.Email
                })
                .ToList();
        }

        public List<AccountGetListResponse> GetUserBySite(Guid? siteID)
        {
            return _dbcontext.AspNetUsers
                .Join(_dbcontext.AspNetUserSites.Where(s => s.SiteId == siteID), u => u.Id, us => us.UserId, (u, us) => new 
                { 
                    User = u,
                    UserSite = us 
                })
                .Join(_dbcontext.AspNetGroupUsers.Where(s => s.SiteId == siteID), uus => uus.UserSite.AspNetGroupUserId, ug => ug.AspNetGroupUserId, (uus, ug) => new
                {
                    uus.User,
                    uus.UserSite,
                    GroupUser = ug
                })
               .Where(s => s.UserSite.Status != UserSiteStatus.DELETED)
               .Select(s => new AccountGetListResponse
               {
                   UserId = s.User.Id,
                   Email = s.User.Email,
                   FullName = s.User.FullName,
                   Avatar = s.User.Avatar,
                   IsActive = s.User.IsActive,
                   GroupUserId = s.GroupUser.AspNetGroupUserId,
                   GroupCode = s.GroupUser.GroupUserCode,
                   GroupRoleLevel = s.GroupUser.RoleLevel,
                   SiteId = s.UserSite.SiteId,
                   UserSiteStatus = s.UserSite.Status,
                   ExpireDate = s.UserSite.ExpireDate.HasValue ? s.UserSite.ExpireDate.Value.AddMinutes(420) : null
               })
               .OrderBy(s => s.GroupRoleLevel)
               .ThenBy(s => s.UserSiteStatus)
               .ThenBy(s => s.FullName)
               .ToList();
        }

        public List<MenuGroupUserSiteResourceResult> GetUserMenusRolesResource(string userID, Guid? siteID)
        {
            return _dbcontext.AspNetUserSites
                .Where(s => s.UserId == userID && ((siteID.HasValue && s.SiteId == siteID) || !s.SiteId.HasValue) && s.Status == UserSiteStatus.ACTIVE)
                .Join(_dbcontext.MenuDefineAspNetGroupUsers, us => us.AspNetGroupUserId, mngu => mngu.AspNetGroupUserId, (us, mngu) => new
                {
                    UserSite = us,
                    MenuGroupUser = mngu,
                })
                .Join(_dbcontext.MenuDefines, mngus => mngus.MenuGroupUser.MenuCode, mn => mn.MenuCode, (mngus, mn) => new
                {
                    mngus.UserSite,
                    mngus.MenuGroupUser,
                    Menu = mn
                })
                .Where(s => (s.Menu.IsActive ?? false) || !s.UserSite.SiteId.HasValue)
                .Select(s =>  new MenuGroupUserSiteResourceResult
                {
                    MenuCode = s.Menu.MenuCode,
                    MenuKeyword = s.Menu.Keyword,
                    MenuParentCode = s.Menu.MenuParentCode,
                    DisplayOrder = s.Menu.MenuOrder,
                    RouterName = s.Menu.RouteName,
                    MenuType = s.Menu.Type,
                    IsAdminView = s.Menu.IsAdminView ?? false,
                    WhenAction = s.MenuGroupUser.WhenAction,
                    ObjectCode = s.Menu.ObjectCode,
                    IconName = s.Menu.IconName
                })
                .OrderBy(s => s.MenuCode)
                .ThenBy(s => s.DisplayOrder)
                .ToList();
        }

        public List<UserResourceResult> GetUserResourseBySite(Guid? siteID)
        {
            return _dbcontext.AspNetUserSites
                .Where(s => s.SiteId == siteID && s.Status != UserSiteStatus.DELETED)
                .Join(_dbcontext.AspNetUsers, us => us.UserId, u => u.Id, (us, u) => new
                {
                    User = u,
                    UserSite = us
                })
                .Select(s => new UserResourceResult
                {
                    UserId = s.User.Id,
                    Email = s.User.Email,
                    FullName = s.User.FullName,
                    UserName = s.User.UserName,
                    Avatar = s.User.Avatar,
                    Status = s.UserSite.Status,
                    ExpiredDate = s.UserSite.ExpireDate.HasValue ? s.UserSite.ExpireDate.Value.AddMinutes(420) : null,
                    Expired = s.UserSite.ExpireDate.HasValue && s.UserSite.ExpireDate.Value.AddMinutes(1439) < DateTime.UtcNow ? true : false
                })
                .ToList();
        }

        public UserSiteRoleNameResult? GetUserSiteRoleName(string userID, Guid? siteID)
        {
            var userGroupSite = _dbcontext.AspNetUserSites
                .Where(s => s.UserId == userID && (!s.SiteId.HasValue || (s.SiteId == siteID)))
                .LeftJoin(_dbcontext.AspNetGroupUsers, us => us.AspNetGroupUserId, ug => ug.AspNetGroupUserId, (us, ug) => new
                {
                    UserSite = us,
                    GroupUser = ug
                })
                .Select(s => new
                {
                    GroupUserId = s.GroupUser.AspNetGroupUserId,
                    s.UserSite.SiteId,
                    s.GroupUser.GroupUserCode,
                    IsSystem = s.GroupUser.IsSystem ?? false,
                    s.GroupUser.RoleLevel
                })
                .ToList();
            var groupUserDetail = userGroupSite.OrderByDescending(s => s.IsSystem).FirstOrDefault();
            if (groupUserDetail != null)
            {
                return new UserSiteRoleNameResult
                {
                    GroupUserID = groupUserDetail.GroupUserId,
                    GroupUserCode = groupUserDetail.GroupUserCode,
                    RoleLevel = groupUserDetail.RoleLevel
                };
            }
            return null;
        }
    }
}