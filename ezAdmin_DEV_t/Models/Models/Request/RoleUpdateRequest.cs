using System.ComponentModel.DataAnnotations;

namespace Models.Models.Request
{
    public class RoleUpdateRequest
    {
        public Guid? RoleId { get; set; }
        [Required]
        public string? MenuCode { get; set; }
        public bool? Status { get; set; }
        public List<RoleUpdateRequest>? Childs { get; set; }
    }
}
