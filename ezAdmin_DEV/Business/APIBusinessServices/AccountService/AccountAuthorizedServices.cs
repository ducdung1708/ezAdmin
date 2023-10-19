using Infrastructure.Helpers;
using Business.APIBusinessServices.ThirtyPartyApp;
using Business.ezID;
using Infrastructure.ConstantsDefine;
using Infrastructure.ConstantsDefine.AppSetting;
using Infrastructure.ConstantsDefine.HardCode;
using Infrastructure.ConstantsDefine.KeywordTranslate;
using Infrastructure.Core.Cache;
using Models.DBContext;
using Models.EntityModels;
using Models.Models.ParamsFunction;
using Models.Models.Request;
using Models.Models.Response;
using Models.Models.Result;
using Models.ThirdParty.ezID.Response;
using Repository.Interfaces;

namespace Business.APIBusinessServices.AccountServices
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountAuthorizedServices : BaseBusinessServices<AccountVerifyRequest, AccountVerifyResponse>
    {
        private readonly ezSQLDBContext _context;
        private readonly IConfiguration _configuration;
        private readonly ICacheService _cacheService;
        private readonly IAspNetUserRepository _aspNetUserRepository;
        private readonly IAspNetUserSiteRepository _aspNetUserSiteRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IAspNetUserSessionRepository _aspNetUserSessionRepository;
        private readonly ezIDAPIServices _ezIDAPIServices;

        private string tokenGenerator;
        private Guid? siteID;
        private DateTime? expiration;
        private Guid? userId;
        private ezIDUserInfoResponse ezIDUserInfoResponse;
        private string? languageCode = LocaleLanguageCodes.VI;
        private AspNetUserSession newUserSession;
        private AspNetUser newUser;
        private AspNetUser? accountDetail;
        private UserSessionCacheResult newCache;
        private List<string> systemGroupCodes = new List<string> { GroupUserCodes.SYSTEM_SUPPORT, GroupUserCodes.SYSTEM_ADMIN };

        private string userIDStr
        {
            get
            {
                return userId.HasValue ? userId.ToString() : null;
            }
        }

        private string siteIDStr
        {
            get
            {
                return siteID.HasValue ? siteID.ToString() : null;
            }
        }

        private string JWT_Secret
        {
            get
            {
                return _configuration.GetValue<string>(JWTKeys.SECRET_KEY);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="_configuration"></param>
        /// <param name="_cacheService"></param>
        public AccountAuthorizedServices(
            ezSQLDBContext context,
            ezIDAPIServices ezIDAPIServices,
            IConfiguration configuration,
            ICacheService cacheService,
            IAspNetUserRepository aspNetUserRepository,
            IAspNetUserSiteRepository aspNetUserSiteRepository,
            ICompanyRepository companyRepository,
            IAspNetUserSessionRepository aspNetUserSessionRepository,
            SlackSendMessageServices slackSendMessageServices,
            IHttpContextAccessor httpContextAccessor,
            string successMessageDefault = AccountKeyword.AUTHORIZED_SUCCESS) 
            : base(slackSendMessageServices, httpContextAccessor, successMessageDefault)
        {
            _context = context;
            _ezIDAPIServices = ezIDAPIServices;
            _configuration = configuration;
            _cacheService = cacheService;
            _aspNetUserRepository = aspNetUserRepository;
            _aspNetUserSiteRepository = aspNetUserSiteRepository;
            _companyRepository = companyRepository;
            _aspNetUserSessionRepository = aspNetUserSessionRepository;
        }

        public override void P1GenerateObjects()
        {
            var getUserResponse = _ezIDAPIServices.GetUserInfo(new ParamsUserInfoezID
            {
                Token = _dataRequest.ezIDToken
            });
            if (getUserResponse.StatusCode != StatusCodes.Status200OK.ToString())
            {
                throw new BaseExceptionResult { StatusCode = getUserResponse.StatusCode,  Messages = getUserResponse.Messages };
            }
            ezIDUserInfoResponse = getUserResponse.Data;
            accountDetail = _aspNetUserRepository
                .GetBy(s => (s.Email ?? "").Trim().ToLower().Equals(ezIDUserInfoResponse.email))
                .FirstOrDefault();
            if (accountDetail == null)
            {
                userId = Guid.NewGuid();
                Tuple<string, DateTime?> createAuthAccountTokenResult = Helper.CreateAuthAccountToken(new ParamsAuthAccountToken
                {
                    UserID = userId,
                    SiteID = null,
                    RoleName = "",
                    SecretKey = JWT_Secret
                });
                tokenGenerator = createAuthAccountTokenResult.Item1;
                expiration = createAuthAccountTokenResult.Item2;
                newUser = new AspNetUser
                {
                    Id = userIDStr,
                    UserName = ezIDUserInfoResponse.preferred_username,
                    Email = ezIDUserInfoResponse.email,
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    FullName = ezIDUserInfoResponse.name,
                    LanguageCode = languageCode,
                    IsActive = true,
                };
                Guid userSessionId = Guid.NewGuid();
                DateTime lastActiveDate = DateTime.UtcNow;
                newUserSession = new AspNetUserSession
                {
                    AspNetUserSessionId = userSessionId,
                    UserId = userIDStr,
                    AuthToken = tokenGenerator,
                    ExpirationDateTime = expiration,
                    LastActiveDate = lastActiveDate,
                };
                newCache = new UserSessionCacheResult
                {
                    AspNetUserSessionId = userSessionId,
                    UserID = userId,
                    Token = tokenGenerator,
                    Expiration = expiration,
                    LastActiveDate = lastActiveDate
                };
            }
            else
            {
                userId = Guid.Parse(accountDetail.Id);
                siteID = accountDetail.LastSiteId;
                languageCode = accountDetail.LanguageCode;
                UserSiteRoleNameResult userSiteRoleName = _aspNetUserRepository.GetUserSiteRoleName(accountDetail.Id, siteID);
                bool isSystemAccount = userSiteRoleName != null && systemGroupCodes.Contains(userSiteRoleName.GroupUserCode);
                if (!isSystemAccount)
                {
                    siteID = GetLastSiteIdUserLogin(userIDStr, siteID);
                }
                Tuple<string, DateTime?> createAuthAccountTokenResult = Helper.CreateAuthAccountToken(new ParamsAuthAccountToken
                {
                    UserID = userId,
                    SiteID = siteID,
                    RoleName = userSiteRoleName != null ? userSiteRoleName.GroupUserCode : null,
                    SecretKey = JWT_Secret
                });
                tokenGenerator = createAuthAccountTokenResult.Item1;
                expiration = createAuthAccountTokenResult.Item2;
                Guid userSessionId = Guid.NewGuid();
                DateTime lastActiveDate = DateTime.UtcNow;
                newUserSession = new AspNetUserSession
                {
                    AspNetUserSessionId = userSessionId,
                    UserId = userIDStr,
                    AuthToken = tokenGenerator,
                    ExpirationDateTime = expiration,
                    LastActiveDate = lastActiveDate,
                    SiteId = siteID,
                };
                newCache = new UserSessionCacheResult
                {
                    AspNetUserSessionId = userSessionId,
                    UserID = userId.Value,
                    SiteID = siteID,
                    Token = tokenGenerator,
                    Expiration = expiration,
                    LastActiveDate = lastActiveDate
                };
            }
        }

        public override void P2PostValidation()
        {

        }

        public override void P3AccessDatabase()
        {
            if (newUser != null)
            {
                _aspNetUserRepository.Add(newUser);
            }
            else if (accountDetail != null)
            {
                accountDetail.Avatar = ezIDUserInfoResponse.avatar_url;
                accountDetail.FullName = ezIDUserInfoResponse.name;
                accountDetail.UserName = ezIDUserInfoResponse.preferred_username;
            }
            if (newUserSession != null)
            {
                _aspNetUserSessionRepository.Add(newUserSession);
            }
            _context.SaveChanges();
            if (newCache != null)
            {
                string hashKey = _cacheService.GetHashKey(RedisCacheKeys.USER_SESSION, tokenGenerator);
                _cacheService.AddOrUpdate(RedisCacheKeys.USER_SESSION, hashKey, newCache);
            }
        }

        public override void P4GenerateResponseData()
        {
            _dataResponse = new AccountVerifyResponse
            {
                UserID = userIDStr,
                SiteID = siteIDStr,
                Token = tokenGenerator,
                Expiration = expiration,
                FullName = ezIDUserInfoResponse.name,
                UserName = ezIDUserInfoResponse.preferred_username,
                Avatar = ezIDUserInfoResponse.avatar_url,
                Email = ezIDUserInfoResponse.email,
                LanguageCode = languageCode
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="siteID"></param>
        /// <returns></returns>
        private Guid? GetLastSiteIdUserLogin(string userID, Guid? siteID)
        {
            if (!siteID.HasValue)
            {
                return null;
            }
            var siteDetail = _companyRepository.GetById<Guid?>(siteID);
            if (siteDetail == null)
            {
                return null;
            }
            if (siteDetail.Status == SiteStatus.DEACTIVE)
            {
                return null;
            }
            var userSiteDetail = _aspNetUserSiteRepository.GetUserSiteDetail(userID, siteID);
            if (userSiteDetail == null)
            {
                return null;
            }
            bool stopUsingAccount = userSiteDetail.Status == UserSiteStatus.DELETED || userSiteDetail.Status == UserSiteStatus.DEACTIVE;
            if (stopUsingAccount)
            {
                return null;
            }
            if (userSiteDetail.ExpireDate.HasValue && userSiteDetail.ExpireDate.Value.AddMinutes(1439) < DateTime.UtcNow)
            {
                return null;
            }
            return siteID;
        }
    }
}
