namespace Models.Models.Others
{
    public class SiteFilterInfo
    {
        public Guid SiteId { get; set; }
        public string SiteName { get; set; }
        public int? PartnerID { get; set; }
        public string PartnerName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string CountryCode { get; set; }
        public string TaxCode { get; set; }
        public string CountryName { get; set; }
        public string Status { get; set; }
        public List<ClientConnectInfo> ClientsConnect { get; set; } = new List<ClientConnectInfo>();
    }
}
