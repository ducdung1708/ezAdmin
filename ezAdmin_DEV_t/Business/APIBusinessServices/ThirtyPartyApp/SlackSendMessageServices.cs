using Infrastructure.ConstantsDefine.AppSetting;
using Infrastructure.Core;
using Models.Models.ParamsFunction;
using Models.ThirdParty.Slack.Others;
using Models.ThirdParty.Slack.Request;

namespace Business.APIBusinessServices.ThirtyPartyApp
{
    public class SlackSendMessageServices
    {
        private string? _webhookURL;
        private string? _token;
        private string? _channelID;

        public SlackSendMessageServices(IConfiguration configuration)
        { 
            _webhookURL = configuration.GetValue<string>(SlackKeys.WEBHOOK_URL);
            _token = configuration.GetValue<string>(SlackKeys.TOKEN);
            _channelID = configuration.GetValue<string>(SlackKeys.CHANNEL_ID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramsMessageSlack"></param>
        public void SendMessageError(ParamsMessageSlack paramsMessageSlack)
        {
            var messageRequest = new PostMessageRequest()
            {
                attachments = new List<SlackAttachment>
                {
                    new SlackAttachment
                    {
                        fallback = "ezINV Web App",
                        color = "danger",
                        title = paramsMessageSlack.Title,
                        image_url = "",
                        text = paramsMessageSlack.Messages
                    }
                }
            };
            if (!string.IsNullOrEmpty(_webhookURL))
            {
                var sendMessageTask = SlackClient.SendMessageByWebhook(_webhookURL, messageRequest);
                Task.WhenAll(sendMessageTask);
            }
            else if (!string.IsNullOrEmpty(_token) && !string.IsNullOrEmpty(_channelID))
            {
                var sendMessageTask = SlackClient.SendMessageChat(_token, messageRequest);
                Task.WhenAll(sendMessageTask);
            }
        }
    }
}
