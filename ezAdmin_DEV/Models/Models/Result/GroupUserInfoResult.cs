namespace Models.Models.Result
{
    public class GroupUserInfoResult
    {
        public Guid AspNetGroupUserID { get; set; }
        public string? GroupUserCode { get; set; }
        public int? GroupLevel { get; set; }
        public string? GroupUserKeyword { get; set; }
        public string? GroupUserDescription { get; set; }
        public Guid? SiteId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedByUserId { get; set; }
    }
}