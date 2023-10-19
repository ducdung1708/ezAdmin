namespace Models.ThirdParty.ezID.Response
{
    public class UserCreateResponse
    {
        public int statusCode { get; set; }
        public string message { get; set; }
        public Guid? ezUserId { get; set; }
        public bool? emailConfirmed { get; set; }
    }
}
