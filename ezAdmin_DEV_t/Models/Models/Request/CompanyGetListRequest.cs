using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Models.Request
{
	public class CompanyGetListRequest
	{
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        
        [Required]
        public int? PageSize { get; set; }

        [Required]
        public int? PageIndex { get; set; }
    }
}

