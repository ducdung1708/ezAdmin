namespace Models.ThirdParty.Slack.Response
{
    public class SlackMessageResponse
    {
        public bool ok { get; set; }
        public string? error { get; set; }
        public string? channel { get; set; }
        public string? ts { get; set; }
    }
}