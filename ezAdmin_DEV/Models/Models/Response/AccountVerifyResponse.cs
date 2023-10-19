namespace Models.Models.Response
{
    public class AccountVerifyResponse
    {
        public string UserID { get; set; }
        public string SiteID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public string LanguageCode { get; set; }
        public string Email { get; set; }
        public bool? IsSysAdmin { get; set; }
        public string Token { get; set; }
        public DateTime? Expiration { get; set; }
    }
}
