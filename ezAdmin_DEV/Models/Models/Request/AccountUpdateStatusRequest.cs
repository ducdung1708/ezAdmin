using System.ComponentModel.DataAnnotations;

namespace Models.Models.Request
{
    public class AccountUpdateStatusRequest
    {
        [Required]
        public string? UserID { get; set; }
        [Required]
        public string? Status { get; set; }
        public string? ReasonDeactive { get; set; }
    }
}