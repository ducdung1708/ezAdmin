using System;
namespace Models.Models.Result
{
	public class DistrictDetailResult
	{
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? AirportCode { get; set; }
        public string? PhoneCode { get; set; }
        public string? ExtraCode { get; set; }
        public string? PostalCode { get; set; }
        public double? Latitude { get; set; }
        public double? Longtitude { get; set; }
        public int? SortIndex { get; set; }
        public int? Active { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public Guid CityId { get; set; }
    }
}

