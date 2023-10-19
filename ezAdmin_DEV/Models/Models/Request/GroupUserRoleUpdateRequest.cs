using System.ComponentModel.DataAnnotations;

namespace Models.Models.Request
{
    public class GroupUserRoleUpdateRequest
    {
        [Required]
        public Guid? GroupUserID { get; set; }

        [Required]
        public List<RoleUpdateRequest> RolesData { get; set; } = new List<RoleUpdateRequest>();
    }
}
