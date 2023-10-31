﻿using System;
namespace Models.Models.Response
{
	public class CompanyGetDetailResponse
	{
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? TaxCode { get; set; }

        public int? PartnerID { get; set; }
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

