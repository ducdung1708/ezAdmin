using Infrastructure.Constants;
using Models.Models.ParamsFunction;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace Infrastructure.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsMatchEmail(this string input)
        {
            const string EmailRegex =
   @"^(?:[\w\!\#\$\%\&\'\*\+\-\/\=\?\^\`\{\|\}\~]+\.)*[\w\!\#\$\%\&\'\*\+\-\/\=\?\^\`\{\|\}\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!\.)){0,61}[a-zA-Z0-9]?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\[(?:(?:[01]?\d{1,2}|2[0-4]\d|25[0-5])\.){3}(?:[01]?\d{1,2}|2[0-4]\d|25[0-5])\]))$";

            var regex = new Regex(EmailRegex);
            return regex.IsMatch(input);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseURL"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        public static string CreateRequestURI(string baseURL, string endPoint)
        {
            if (!string.IsNullOrEmpty(baseURL) && !string.IsNullOrEmpty(endPoint))
            {
                string _baseURL = baseURL;
                if (!_baseURL.EndsWith("/"))
                {
                    _baseURL += "/";
                }
                return $"{_baseURL}{endPoint}";
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramsAuthAccountToken"></param>
        /// <returns></returns>
        public static Tuple<string, DateTime?> CreateAuthAccountToken(ParamsAuthAccountToken paramsAuthAccountToken)
        {
            var authClaims = new List<Claim>();
            if (paramsAuthAccountToken.UserID.HasValue)
            {
                authClaims.Add(new Claim(ClaimExtension.USER_ID, paramsAuthAccountToken.UserID.ToString()));
            }
            if (paramsAuthAccountToken.SiteID.HasValue)
            {
                authClaims.Add(new Claim(ClaimExtension.SITE_ID, paramsAuthAccountToken.SiteID.ToString()));
            }
            string roleName = paramsAuthAccountToken.RoleName;
            if (!string.IsNullOrEmpty(roleName))
            {
                authClaims.Add(new Claim(ClaimExtension.ROLE_NAME, roleName));
            }
            if (paramsAuthAccountToken.Claims != null)
            {
                authClaims.AddRange(paramsAuthAccountToken.Claims);
            }
            string JWT_Secret = paramsAuthAccountToken.SecretKey;
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT_Secret));
            var token = new JwtSecurityToken(
                notBefore: DateTime.Now,
                expires: paramsAuthAccountToken.Expires.HasValue ? paramsAuthAccountToken.Expires : DateTime.Now.AddDays(30),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            string tokenWrite = new JwtSecurityTokenHandler().WriteToken(token);
            DateTime? tokenExpiration = token.ValidTo;
            return Tuple.Create(tokenWrite, tokenExpiration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ObjectToString(object data)
        {
            if (data != null)
            {
                return JsonConvert.SerializeObject(data);
            }
            return "null";
        }

        public static string FormatException(Exception ex)
        {
            string exceptionDisplay = "";
            if (!string.IsNullOrEmpty(ex.Message))
            {
                exceptionDisplay += $"Message: " +
                    $"\n>{ex.Message}";
            }
            if (ex.InnerException != null || ex.StackTrace != null)
            {
                exceptionDisplay += $"\nException: ";
            }
            if (ex.InnerException != null)
            {
                exceptionDisplay += $"\n>```{Helper.ObjectToString(ex.InnerException)}```";
            }
            if (ex.StackTrace != null)
            {
                exceptionDisplay += $"\n>```{Helper.ObjectToString(ex.StackTrace)}```";
            }
            return exceptionDisplay;
        }

        /// <summary>
        /// Convert object to dictionary
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Dictionary<string, TValue> ToDictionary<TValue>(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, TValue>>(json);
            return dictionary;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramsMessageChatForHttpClient"></param>
        /// <returns></returns>
        public static string CreateMessageChat(ParamsMessageChatForHttpClient paramsMessageChatForHttpClient)
        {
            string requestURI = paramsMessageChatForHttpClient.RequestURI;
            Dictionary<string, string> headers = paramsMessageChatForHttpClient.Headers ?? new Dictionary<string, string>();
            object request = paramsMessageChatForHttpClient.Request;
            object response = paramsMessageChatForHttpClient.Response;
            Exception exception = paramsMessageChatForHttpClient.Excetion;
            string headerContent = headers.Count() > 0 ? $"\nHeader: \n>```{Helper.ObjectToString(headers)}```" : "";
            string resquestContent = request != null ? $"\nData Request: \n>```{Helper.ObjectToString(request)}```" : "";
            string responseContent = response != null ? $"\nResponse: \n>```{Helper.ObjectToString(response)}```" : "";
            string exceptionContent = exception != null ? Helper.FormatException(exception) : "";
            return $"Uri: \n```{requestURI}```" +
                $"{headerContent}" +
                $"{resquestContent}" +
                $"{responseContent}" +
                $"{exceptionContent}";
        }

        /// <summary>
        /// Lấy ra Sub domain
        /// </summary>
        /// <param name="appPath"></param>
        /// <returns></returns>
        public static string GetSubDomain(string appPath)
        {
            try
            {
                Uri myUri = new Uri(appPath);
                string hostname = myUri.Authority;
                char[] separators = new char[] { '.' };
                string[] domains = (hostname ?? "").Split(separators);
                if (domains.Count() > 0)
                {
                    return domains[0];
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
