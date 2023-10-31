using Models.Models.Result;

namespace Models.Models.Response
{
    public class ResourceAppResponse
    {
        public List<MenuGroupUserSiteResourceResult> MenusRoles { get; set; }
        public List<UserSiteResourceResult> Sites { get; set; }
        public List<GroupUserSiteResult> GroupUsers { get; set; } = new List<GroupUserSiteResult>();
    }
}
