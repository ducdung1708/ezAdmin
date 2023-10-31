using Infrastructure.ConstantsDefine.HardCode;
using Models.DBContext;
using Models.EntityModels;
using Models.Models.Result;
using Repository.Interfaces;

namespace Repository.Implementation
{
    public class RolesRepository : RepositoryBase<MenuDefineAspNetGroupUser>, IRolesRepository
    {
        private readonly ezSQLDBContext _dbcontext;

        public RolesRepository(ezSQLDBContext dbcontext) : base(dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public List<RolesResult> GetMenuByGroupId(Guid? GroupUserID)
        {

            return _dbcontext.MenuDefineAspNetGroupUsers
                .Where(s => s.AspNetGroupUserId == GroupUserID)
                .Join(_dbcontext.MenuDefines, mngu => mngu.MenuCode, m => m.MenuCode, (mngu, m) => new
                {
                    MenuGroupUser = mngu,
                    Menu = m
                })
                .Where(s => (s.Menu.IsActive ?? false))
                .Select(s => new RolesResult
                {
                    RoleId = s.MenuGroupUser.MenuDefineAspNetGroupUserId,
                    Icon = s.Menu.IconName,
                    Keyword = s.Menu.Keyword,
                    MenuCode = s.Menu.MenuCode,
                    MenuParentCode = s.Menu.MenuParentCode,
                    MenuOrder = s.Menu.MenuOrder,
                    Type = s.Menu.Type
                })
                .ToList();
        }

        public List<GroupRoleTemplatesResult> GetGroupRoleTemplates()
        {
            return _dbcontext.AspNetGroupUsers
                .Where(s => !(s.IsSystem ?? false) && !s.SiteId.HasValue)
                .Join(_dbcontext.MenuDefineAspNetGroupUsers, g => g.AspNetGroupUserId, mng => mng.AspNetGroupUserId, (g, mng) => new
                {
                    MenuGroupUser = mng,
                    GroupUser = g
                })
                .Select(s => new GroupRoleTemplatesResult
                {
                    GroupUserCode = s.GroupUser.GroupUserCode,
                    MenuCode = s.MenuGroupUser.MenuCode,
                    CreatedDate = DateTime.UtcNow,
                    WhenAction = s.MenuGroupUser.WhenAction
                })
                .ToList();
        }
    }
}
