using Models.EntityModels;
using Models.Models.Result;

namespace Repository.Interfaces
{
    public interface IRolesRepository : IRepositoryBase<MenuDefineAspNetGroupUser>
    {
        List<RolesResult> GetMenuByGroupId(Guid? GroupUserID);
        List<GroupRoleTemplatesResult> GetGroupRoleTemplates();
    }
}
