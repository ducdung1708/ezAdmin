namespace Models.Models.Result
{
    public class UserSiteResourceResult
    {
        public Guid SiteID { get; set; }
        public string? SiteName { get; set; }
        public string? TaxCode { get; set; }
        public string? Avatar { get; set; }
        public string? GroupRoleCode { get; set; }
        public int? GroupRoleLevel { get; set; }
        public string? GroupRoleKeyword { get; set; }
    }
}
