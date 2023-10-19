namespace Models.ThirdParty.Slack.Others
{
    public class SlackAttachment
    {
        public string? fallback { get; set; }
        public string? text { get; set; }
        public string? title { get; set; }
        public string? image_url { get; set; }
        public string? color { get; set; }
    }
}