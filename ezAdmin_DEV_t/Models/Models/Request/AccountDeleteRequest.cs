using System.ComponentModel.DataAnnotations;

namespace Models.Models.Request
{
    public class AccountDeleteRequest
    {
        [Required]
        public string? UserID { get; set; }

        [Required]
        public string? ReasonDelete { get; set; }
    }
}