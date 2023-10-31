using System.ComponentModel.DataAnnotations;

namespace Models.Models.Request
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountVerifyRequest
    {
        /// <summary>
        /// Token từ ezID
        /// </summary>
        [Required]
        public string? ezIDToken { get; set; }
    }
}
