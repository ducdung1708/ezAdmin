using System.ComponentModel.DataAnnotations;

namespace Models.Models.Others
{
    public class SerialRoleUserInfo
    {
        [Required(ErrorMessage = "userid_is_required")]
        public string UserId { get; set; }
    }
}
