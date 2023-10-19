using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Models.Request
{
    public class CompanyUpdateRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string? Name { get; set; }

        public string? Address { get; set; }

        [Required]
        [MaxLength(200)]
        public string? Email { get; set; }
        public string? Phone { get; set; }

        [Required]
        [MaxLength(200)]
        public string? TaxCode { get; set; }

        public int? Partner { get; set; }
        public string? PartnerUrl { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Username2 { get; set; }
        public string? Password2 { get; set; }
        public string? PartnerUrl2 { get; set; }

        public string? Avatar { get; set; }
        public string? CountryCode { get; set; }
        public string? Status { get; set; }
        public string? PartnerConnectStatus { get; set; }
    }
}

