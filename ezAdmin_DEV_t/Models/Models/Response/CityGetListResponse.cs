using System;
namespace Models.Models.Response
{
	public class CityGetListResponse
	{
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? PhoneCode { get; set; }
        public string? ExtraCode { get; set; }
        public int Active { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

