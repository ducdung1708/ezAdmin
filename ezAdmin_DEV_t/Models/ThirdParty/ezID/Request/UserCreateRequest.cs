namespace Models.ThirdParty.ezID.Request
{
    public class UserCreateRequest
    {
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string name { get; set; }
        public string website { get; set; }
        public string streetAddress { get; set; }
        public string postalCode { get; set; }
        public string country { get; set; }
        public string fromSource { get; set; } = "ezInvoice";
        public string fromSourceUri { get; set; }
    }
}
