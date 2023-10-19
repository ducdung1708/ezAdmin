using System;
using System.ComponentModel.DataAnnotations;
namespace Models.Models.Request
{

    public class DistrictGetListRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        [Required]
        public int? PageSize { get; set; }
        [Required]
        public int? PageIndex { get; set; }
        //public string? AirportCode { get; set; }
        //public string? PhoneCode { get; set; }
        //public string? ExtraCode { get; set; }
        //public string? PostalCode { get; set; }
        //public string? Latitude { get; set; }
        //public string? Longtitude { get; set; }
        //public string? SortIndex { get; set; }
        //public string? Active { get; set; }
        //public string? Longtitude { get; set; }
        //public string? Longtitude { get; set; }
    }

}

