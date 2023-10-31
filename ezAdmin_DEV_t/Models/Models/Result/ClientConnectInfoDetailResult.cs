using Models.Models.Others;

namespace Models.Models.Result
{
    public class ClientConnectInfoDetailResult : ClientConnectInfo
    {
        public Guid SiteClientConnectionID { get; set; }
        public string Token { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
