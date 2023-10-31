namespace Models.Models.Result
{
    public class UserResourceResult
    {
        public string? UserId { get; set; }
        public string? Avatar { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        public string? Status { get; set; }

        public bool? Expired { get; set; }

        public DateTime? ExpiredDate { get; set; }
    }
}
