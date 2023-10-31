using Models.ThirdParty.Slack.Others;

namespace Models.ThirdParty.Slack.Request
{
    public class PostMessageRequest
    {
        public string? channel { get; set; }
        public string? text { get; set; }
        public bool as_user { get; set; }
        public List<SlackAttachment> attachments { get; set; } = new List<SlackAttachment>();
    }
}
