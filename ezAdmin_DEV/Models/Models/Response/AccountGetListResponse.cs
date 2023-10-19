namespace Models.Models.Response
{
    public class AccountGetListResponse
    {
        public string UserId { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? Avatar { get; set; }
        public bool IsActive { get; set; }
        public Guid? GroupUserId { get; set; }
        public string? GroupCode { get; set; }
        public int? GroupRoleLevel { get; set; }
        public string UserSiteStatus { get; set; }
        public DateTime? ExpireDate { get; set; }
        public Guid? SiteId { get; set; }
    }
}
