namespace Models.Models.Result
{
    public class RolesResult
    {
        public Guid RoleId { get; set; }
        public string? Keyword { get; set; }
        public string? Icon { get; set; }
        public string? MenuCode { get; set; }
        public string? MenuParentCode { get; set; }
        public int? MenuOrder { get; set; }
        public string? Type { get; set; }
    }
}
