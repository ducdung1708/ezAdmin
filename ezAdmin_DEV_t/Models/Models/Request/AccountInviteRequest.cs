using System.ComponentModel.DataAnnotations;

namespace Models.Models.Request
{
    public class AccountInviteRequest
    {
        [Required]
        [MaxLength(150)]
        public string? Email { get; set; }

        [Required]
        public Guid? GroupUserID { get; set; }

        [MaxLength(200)]
        public string? Messages { get; set; }
    }
}
