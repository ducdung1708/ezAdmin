namespace Models.Models.Response
{
    public class RolesGroupUserResponse
    {
        public Guid? RoleId { get; set; }
        public string? Keyword { get; set; }
        public string? Icon { get; set; }
        public bool? Status { get; set; }
        public string MenuCode { get; set; }
        public string Type { get; set; }
        public bool? Indeterminate { get; set; }
        public List<RolesGroupUserResponse>? Childs { get; set; }
    }
}
