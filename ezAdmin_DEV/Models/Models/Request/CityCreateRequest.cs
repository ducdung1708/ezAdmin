using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Models.Request
{
	public class CityCreateRequest
	{
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string? Code { get; set; }

        public string? AirportCode { get; set; }
        public string? PhoneCode { get; set; }
        public string? PostalCode { get; set; }
        public string? ExtraCode { get; set; }

        public double? Latitude { get; set; }
        public double? Longtitude { get; set; }
        public int? SortIndex { get; set; }
        public int? Active { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }

        public int? CountryId { get; set; }
    }
}

