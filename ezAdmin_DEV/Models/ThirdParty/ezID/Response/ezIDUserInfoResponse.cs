namespace Models.ThirdParty.ezID.Response
{
    public class ezIDUserInfoResponse
    {
        public Guid sub { get; set; }
        public string email { get; set; }
        public bool? email_verified { get; set; }
        public string address { get; set; }
        public string website { get; set; }
        public string avatar_url { get; set; }

        /// <summary>
        /// Tên đầy đủ của người dùng
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Tên đăng nhập. Thường trùng với email.
        /// </summary>
        public string preferred_username { get; set; }
    }

    public class ezIDUserInfoAddress
    {
        public string street_address { get; set; }
        public string locality { get; set; }
        public string postal_code { get; set; }
        public string country { get; set; }
    }
}
