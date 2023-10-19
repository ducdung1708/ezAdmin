using System.Security.Claims;

namespace Models.Models.ParamsFunction
{
    public class ParamsAuthAccountToken
    {
        public Guid? UserID { get; set; }
        public Guid? SiteID { get; set; }
        public string RoleName { get; set; }
        public string SecretKey { get; set; }
        public DateTime? Expires { get; set; }
        public List<Claim> Claims { get; set; } = new List<Claim>();
    }
}
