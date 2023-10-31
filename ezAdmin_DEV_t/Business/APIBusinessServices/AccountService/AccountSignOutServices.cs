using Business.APIBusinessServices.ThirtyPartyApp;
using Infrastructure.ConstantsDefine;
using Infrastructure.ConstantsDefine.KeywordTranslate;
using Infrastructure.Core.Cache;
using Models.Models.Result;
using Repository.Interfaces;

namespace Business.APIBusinessServices.Account
{
    public class AccountSignOutServices : BaseBusinessServices<object, object>
    {
        private IAspNetUserSessionRepository _aspNetUserSessionRepository;
        private readonly ICacheService _cacheService;
        private UserSessionCacheResult userSessionCacheDelete;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aspNetUserSessionRepository"></param>
        /// <param name="cacheService"></param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="successMessageDefault"></param>
        public AccountSignOutServices(
            IAspNetUserSessionRepository aspNetUserSessionRepository,
            ICacheService cacheService,
            SlackSendMessageServices slackSendMessageServices,
            IHttpContextAccessor httpContextAccessor,
            string successMessageDefault = AccountKeyword.SIGNOUT_SUCCESS)
            : base(slackSendMessageServices, httpContextAccessor, successMessageDefault)
        {
            _aspNetUserSessionRepository = aspNetUserSessionRepository;
            _cacheService = cacheService;
        }

        private string hashKeyUserSession 
        {
            get 
            {
                return _cacheService.GetHashKey(RedisCacheKeys.USER_SESSION, AccessToken);
            }
        }

        public override void P1GenerateObjects()
        {
            if (!UserID.HasValue || string.IsNullOrEmpty(AccessToken))
            {
                throw new BaseExceptionResult { Messages = "account_invalid" };
            }
            userSessionCacheDelete = _cacheService.Get<UserSessionCacheResult>(RedisCacheKeys.USER_SESSION, hashKeyUserSession);
        }

        public override void P2PostValidation()
        {
            
        }

        public override void P3AccessDatabase()
        {
            if (userSessionCacheDelete != null)
            {
                _aspNetUserSessionRepository.Delete<Guid>(userSessionCacheDelete.AspNetUserSessionId);
                _aspNetUserSessionRepository.Save();
                _cacheService.Remove(RedisCacheKeys.USER_SESSION, hashKeyUserSession);
            }
        }

        public override void P4GenerateResponseData()
        {
        }
    }
}
