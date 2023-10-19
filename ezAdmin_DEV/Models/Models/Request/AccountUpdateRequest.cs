using System.ComponentModel.DataAnnotations;

namespace Models.Models.Request
{
    public class AccountUpdateRequest
    {
        [Required]
        public string? UserID { get; set; }

        [Required]
        public Guid? GroupUserID { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}