using Business.APIBusinessServices.ThirtyPartyApp;
using Infrastructure.ConstantsDefine.AppSetting;
using Infrastructure.ConstantsDefine.EndPoint;
using Infrastructure.Helpers;
using Models.Models.Others;
using Models.Models.ParamsFunction;
using Models.Models.Response;
using Models.ThirdParty.ezID.Request;
using Models.ThirdParty.ezID.Response;
using Newtonsoft.Json;
using System.Net;
using System.Reflection;

namespace Business.ezID
{
    public class ezIDAPIServices
    {
        private readonly SlackSendMessageServices _slackSendMessageServices;
        private string? _clientID;
        private string? _secret;
        private string? _ezIDDomain;
        private string? _secretApiKey;
        private string? _fromSourceUri;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public ezIDAPIServices(IConfiguration configuration, SlackSendMessageServices slackSendMessageServices) 
        {
            _ezIDDomain = configuration.GetValue<string>(ezIDKeys.DOMAIN);
            _secretApiKey = configuration.GetValue<string>(ezIDKeys.SECRET_API_KEY);
            _clientID = configuration.GetValue<string>(ezIDKeys.CLIENT_ID);
            _secret = configuration.GetValue<string>(ezIDKeys.SECRET_KEY);
            _fromSourceUri = configuration.GetValue<string>(ezIDKeys.FROM_SOURCE_URI);
            _slackSendMessageServices = slackSendMessageServices;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramsUserInfoezID"></param>
        /// <returns></returns>
        public BaseResponse<ezIDUserInfoResponse> GetUserInfo(ParamsUserInfoezID paramsUserInfoezID)
        {
            string requestURI = Helper.CreateRequestURI(_ezIDDomain, ezIDEndPoint.USER_INFO);
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {paramsUserInfoezID.Token}" },
                { "Accept", "application/json" }
            };
            var taskPost = Post<ParamsUserInfoezID, ezIDUserInfoResponse>(new HttpClientOption
            {
                Uri = requestURI,
                Headers = headers
            }, paramsUserInfoezID);
            Task.WhenAll(taskPost);
            return taskPost.Result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userCreateRequest"></param>
        /// <returns></returns>
        public BaseResponse<UserCreateResponse> CreateUser(UserCreateRequest userCreateRequest)
        {
            userCreateRequest.fromSourceUri = _fromSourceUri;
            string requestURI = Helper.CreateRequestURI(_ezIDDomain, ezIDEndPoint.CREATE_USER);
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Accept", "application/json" },
                { "x-api-key", _clientID },
                { "secret-api-key", _secretApiKey }
            };
            var taskPost = Post<UserCreateRequest, UserCreateResponse>(new HttpClientOption
            {
                Uri = requestURI,
                Headers = headers
            }, userCreateRequest);
            Task.WhenAll(taskPost);
            return taskPost.Result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userCreateRequest"></param>
        /// <returns></returns>
        public BaseResponse<TokenResponse> Token(TokenRequest tokenRequest)
        {
            string requestURI = Helper.CreateRequestURI(_ezIDDomain, ezIDEndPoint.TOKEN);
            try
            {
                tokenRequest.client_id = _clientID;
                tokenRequest.client_secret = _secret;
                Dictionary<string, string> fromData = tokenRequest
                    .GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .ToDictionary(prop => prop.Name, prop => (string)prop.GetValue(tokenRequest, null));
                var taskPost = Post<Dictionary<string, string>, TokenResponse>(new HttpClientOption
                {
                    Uri = requestURI,
                }, fromData);
                Task.WhenAll(taskPost);
                return taskPost.Result;
            }
            catch (Exception ex)
            {
                string contentChatMessage = Helper.CreateMessageChat(new ParamsMessageChatForHttpClient
                {
                    RequestURI = requestURI,
                    Request = tokenRequest,
                    Excetion = ex
                });
                _slackSendMessageServices.SendMessageError(new ParamsMessageSlack
                {
                    Title = "API Token - Server Process Data Fail",
                    Messages = contentChatMessage
                });
                return new BaseResponse<TokenResponse>
                {
                    StatusCode = StatusCodes.Status500InternalServerError.ToString(),
                    Messages = "Server Process Data Fail"
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="httpOptions"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<BaseResponse<T2>> Post<T1, T2>(HttpClientOption httpOptions, T1 request)
        {
            BaseResponse<T2> response = new BaseResponse<T2>
            {
                StatusCode = StatusCodes.Status200OK.ToString()
            };
            string requestURI = httpOptions.Uri;
            string titleChatMessage = "";
            Exception exception = null;
            Dictionary<string, string> headers = httpOptions.Headers ?? new Dictionary<string, string>();
            object responseHttp = null;
            try
            {
                using (var client = new HttpClient())
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    client.DefaultRequestHeaders.Accept.Clear();
                    foreach (KeyValuePair<string, string> entry in headers)
                    {
                        client.DefaultRequestHeaders.Add(entry.Key, entry.Value);
                    }
                    string serializedBodyData = JsonConvert.SerializeObject(request);
                    HttpContent content = null;
                    if (request != null)
                    {
                        if (httpOptions.UseFormData ?? false)
                        {
                            content = new FormUrlEncodedContent(Helper.ToDictionary<string>(request));
                        }
                        else
                        {
                            content = new StringContent(serializedBodyData, System.Text.Encoding.Unicode, "application/json");
                        }
                    }
                    var responseMessage = client.PostAsync(requestURI, content).GetAwaiter().GetResult();
                    var jsonString = await responseMessage.Content.ReadAsStringAsync();
                    if (responseMessage.StatusCode == HttpStatusCode.OK)
                    {
                        T2 responseData = JsonConvert.DeserializeObject<T2>(jsonString);
                        response.StatusCode = StatusCodes.Status200OK.ToString();
                        response.Data = responseData;
                    }
                    else
                    {
                        titleChatMessage = "ezID API - Request Fail";
                        responseHttp = jsonString;
                        response.StatusCode = ((int)responseMessage.StatusCode).ToString();
                        response.Messages = "Request Fail";
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                titleChatMessage = "ezID API - Server Process Data Fail";
                response.StatusCode = StatusCodes.Status500InternalServerError.ToString();
                response.Messages = "Server Process Data Fail";
            }
            if (!string.IsNullOrEmpty(titleChatMessage))
            {
                string contentChatMessage = Helper.CreateMessageChat(new ParamsMessageChatForHttpClient
                {
                    RequestURI = requestURI,
                    Headers = headers,
                    Request = request,
                    Response = responseHttp,
                    Excetion = exception
                });
                _slackSendMessageServices.SendMessageError(new ParamsMessageSlack
                {
                    Title = titleChatMessage,
                    Messages = contentChatMessage
                });
            }
            return response;
        }
    }
}
