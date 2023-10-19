using Infrastructure.ConstantsDefine.EndPoint;
using Models.ThirdParty.Slack.Request;
using Models.ThirdParty.Slack.Response;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Infrastructure.Core
{
    public class SlackClient
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task SendMessageChat(string token, PostMessageRequest msg)
        {
            try
            {
                var content = JsonConvert.SerializeObject(msg);
                var httpContent = new StringContent(
                    content,
                    Encoding.UTF8,
                    "application/json"
                );
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.PostAsync(SlackAPI.CHAT_POST_MESSAGE, httpContent);
                var responseJson = await response.Content.ReadAsStringAsync();
                SlackMessageResponse messageResponse = JsonConvert.DeserializeObject<SlackMessageResponse>(responseJson);
                if (messageResponse.ok == false)
                {
                    throw new Exception("Failed to send message. Error: {messageResponse.error}");
                }
            }
            catch (Exception)
            {
            }
        }

        public static async Task SendMessageByWebhook(string webhookUrl, PostMessageRequest msg)
        {
            try
            {
                var content = JsonConvert.SerializeObject(msg);
                var httpContent = new StringContent(
                    content,
                    Encoding.UTF8,
                    "application/json"
                );
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var response = await client.PostAsync(webhookUrl, httpContent);
                string responseData = await response.Content.ReadAsStringAsync();
                if (responseData != "ok")
                {
                    throw new Exception($"Failed to send message. Error: {responseData}");
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
