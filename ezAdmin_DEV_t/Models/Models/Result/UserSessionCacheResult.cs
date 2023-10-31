namespace Models.Models.Result
{
    public class UserSessionCacheResult
    {
        public Guid AspNetUserSessionId { get; set; }
        public Guid? UserID { get; set; }
        public string? Token { get; set; }
        public DateTime? Expiration { get; set; }
        public DateTime? LastActiveDate { get; set; }
        public Guid? SiteID { get; set; }
    }
}
