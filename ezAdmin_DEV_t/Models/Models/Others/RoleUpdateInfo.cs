namespace Models.Models.Others
{
    public class RoleUpdateInfo
    {
        public Guid? RoleID { get; set; }
        public string? MenuCode { get; set; }
        public Guid GroupUserID { get; set; }

        public bool Status { get; set; }
    }
}
