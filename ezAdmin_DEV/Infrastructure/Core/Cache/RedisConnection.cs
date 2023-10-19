using Infrastructure.ConstantsDefine.AppSetting;
using Infrastructure.Helpers;
using Models.Models.ParamsFunction;
using Models.ThirdParty.Slack.Others;
using Models.ThirdParty.Slack.Request;
using StackExchange.Redis;

namespace Infrastructure.Core.Cache
{
    /// <summary>
    /// 
    /// </summary>
    public class RedisConnection
    {
        private ConnectionMultiplexer _connectionMultiplexer;
        private IDatabase _database;
        private string _configurationString;
        private int _currentDatabaseId = 0;
        private string? _webhookURL;
        private string? _token;
        private string? _channelID;

        /// <summary>
        /// 
        /// </summary>
        public RedisConnection(IConfiguration configuration)
        {
            try
            {
                _webhookURL = configuration.GetValue<string>(SlackKeys.WEBHOOK_URL);
                _token = configuration.GetValue<string>(SlackKeys.TOKEN);
                _channelID = configuration.GetValue<string>(SlackKeys.CHANNEL_ID);
                _configurationString = configuration.GetValue<string>(RedisKeys.CONNECTION_STRING);
                _connectionMultiplexer = ConnectionMultiplexer.Connect(_configurationString);
                _database = _connectionMultiplexer.GetDatabase(_currentDatabaseId);
            }
            catch (Exception ex)
            {
                SendMessageError(new ParamsMessageSlack
                {
                    Title = "Redis Connection Error",
                    Messages = $"ConnectionString: " +
                    $"\n>```{_configurationString}```" +
                    $"\n{Helper.FormatException(ex)}"
                });
            }
        }

        public IDatabase Database => _database;

        /// <summary>
        /// 
        /// </summary>
        public void FlushDatabase()
        {
            _connectionMultiplexer.GetServer(_configurationString).FlushDatabase(_currentDatabaseId);
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