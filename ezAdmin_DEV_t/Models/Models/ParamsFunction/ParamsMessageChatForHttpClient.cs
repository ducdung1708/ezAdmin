namespace Models.Models.ParamsFunction
{
    public class ParamsMessageChatForHttpClient
    {
        public string RequestURI { get; set; }
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
        public object Request { get; set; }
        public object Response { get; set; }
        public Exception Excetion { get; set; }
    }
}
