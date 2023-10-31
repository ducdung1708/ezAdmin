namespace Models.Models.Others
{
    public class HttpClientOption
    {
        public string Uri { get; set; }
        public bool? UseFormData { get; set; }
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
    }
}
