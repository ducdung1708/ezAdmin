using Infrastructure.Helpers;
using Business.APIBusinessServices.ThirtyPartyApp;
using Infrastructure.Constants;
using Infrastructure.ConstantsDefine.HardCode;
using Infrastructure.ConstantsDefine.KeywordTranslate;
using Models.Models.ParamsFunction;
using Models.Models.Response;
using Models.Models.Result;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Business.APIBusinessServices
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T1">Request Model</typeparam>
    /// <typeparam name="T2">Response Model</typeparam>
    public abstract class BaseBusinessServices<T1, T2>
    {
        private readonly SlackSendMessageServices _slackSendMessageServices;
        private IHttpContextAccessor _httpContextAccessor;
        public T1 _dataRequest;
        public T2 _dataResponse;
        public T2 _exceptionDataResponse;
        public BaseResponse<T2> _responseResult;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="successMessageDefault"></param>
        protected BaseBusinessServices(
            SlackSendMessageServices slackSendMessageServices,
            IHttpContextAccessor httpContextAccessor,
            string successMessageDefault = "")
        {
            _slackSendMessageServices = slackSendMessageServices;
            _httpContextAccessor = httpContextAccessor;
            _responseResult = new BaseResponse<T2>()
            {
                StatusCode = StatusCodes.Status200OK.ToString(),
                Messages = successMessageDefault
            };
        }

        /// <summary>
        /// 
        /// </summary>
        public IHttpContextAccessor HttpContextAccessor
        {
            get
            {
                return _httpContextAccessor;
            }
        }

        public string? UserIDStr
        {
            get
            {
                return UserID.HasValue ? UserID.Value.ToString() : null;
            }
        }

        public string? SiteIDStr
        {
            get
            {
                return SiteID.HasValue ? SiteID.Value.ToString() : null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Guid? UserID
        {
            get
            {
                string userID = GetClaimValue(HttpContextAccessor, ClaimExtension.USER_ID);
                if (!string.IsNullOrEmpty(userID))
                {
                    return Guid.Parse(userID);
                }
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Guid? SiteID
        {
            get
            {
                string siteID = GetClaimValue(HttpContextAccessor, ClaimExtension.SITE_ID);
                if (!string.IsNullOrEmpty(siteID))
                {
                    return Guid.Parse(siteID);
                }
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Guid? ezCloudClientID
        {
            get
            {
                string clientID = GetClaimValue(HttpContextAccessor, ClaimExtension.CLIENT_ID);
                if (!string.IsNullOrEmpty(clientID))
                {
                    return Guid.Parse(clientID);
                }
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string? RoleName
        {
            get
            {
                return GetClaimValue(HttpContextAccessor, ClaimExtension.ROLE_NAME);
            }
        }

        public string? AccessToken
        {
            get
            {
                try
                {
                    return _httpContextAccessor.HttpContext.GetTokenAsync("access_token").Result;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public bool IsSystemAdmin
        {
            get
            {
                return RoleName == GroupUserCodes.SYSTEM_ADMIN;
            }
        }

        public bool IsSystemSupport
        {
            get
            {
                return RoleName == GroupUserCodes.SYSTEM_SUPPORT;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="claimName"></param>
        /// <returns></returns>
        private string? GetClaimValue(IHttpContextAccessor httpContextAccessor, string claimName)
        {
            try
            {
                var identity = (ClaimsIdentity)httpContextAccessor.HttpContext.User.Identity;
                IEnumerable<Claim> claims = identity.Claims;
                var claimsDetail = claims.FirstOrDefault(c => c.Type == claimName);
                if (claimsDetail != null)
                {
                    return claimsDetail.Value;
                } 
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Pre-process data to make it beautiful and valid
        /// </summary>
        public abstract void P1GenerateObjects();

        /// <summary>
        /// Check data if it is valid or not
        /// </summary>
        public abstract void P2PostValidation();

        /// <summary>
        /// Access DB to summary data as request
        /// </summary>
        /// <returns></returns>
        public abstract void P3AccessDatabase();

        /// <summary>
        /// Make a response object
        /// </summary>
        public abstract void P4GenerateResponseData();
         
        /// <summary>
        /// Xử lý
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public BaseResponse<T2> Process(T1 dataRequest = default(T1))
        {
            try
            {
                // Auth
                _dataRequest = dataRequest;
                P1GenerateObjects();
                P2PostValidation();
                P3AccessDatabase();
                P4GenerateResponseData();
                _responseResult.Data = _dataResponse;
                return _responseResult;
            }
            catch (BaseExceptionResult ex)
            {
                return new BaseResponse<T2>
                {
                    StatusCode = ex.StatusCode,
                    Messages = ex.Messages,
                    MessagesDetails = ex.MessagesDetails,
                    Data = _exceptionDataResponse,
                };
            }
            catch (Exception ex)
            {
                _slackSendMessageServices.SendMessageError(new ParamsMessageSlack
                {
                    Title = "API Error",
                    Messages = $"Data Request: " +
                    $"\n>```{Helper.ObjectToString(dataRequest)}```" +
                    $"\n{Helper.FormatException(ex)}"
                });
                // Exception Log
                return new BaseResponse<T2>
                {
                    StatusCode = StatusCodes.Status500InternalServerError.ToString(),
                    Messages = CommonKeywords.PROCESS_DATA_ERROR
                };
            }
        }
    }
}
