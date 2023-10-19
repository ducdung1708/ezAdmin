using System.ComponentModel.DataAnnotations;

namespace Models.Models.Request
{
    public class AccountChangeRoleGroupRequest
    {
        [Required]
        public string? UserID { get; set; }

        [Required]
        public Guid? GroupUserID { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Password { get; set; }
    }
}
