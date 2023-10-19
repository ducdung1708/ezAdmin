using System;
namespace Models.Models.Response
{
	public class DistrictGetListResponse
	{
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public Guid CityId { get; set; }
        public string? PhoneCode { get; set; }
        public string? ExtraCode { get; set; }
        public int Active { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

