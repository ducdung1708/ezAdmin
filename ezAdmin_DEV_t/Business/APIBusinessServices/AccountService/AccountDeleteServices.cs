using Business.APIBusinessServices.ThirtyPartyApp;
using Infrastructure.ConstantsDefine;
using Infrastructure.ConstantsDefine.HardCode;
using Infrastructure.ConstantsDefine.KeywordTranslate;
using Infrastructure.Core.Cache;
using Models.DBContext;
using Models.EntityModels;
using Models.Models.Request;
using Models.Models.Result;
using Repository.Interfaces;

namespace Business.APIBusinessServices.Account
{
    public class AccountDeleteServices : BaseBusinessServices<AccountDeleteRequest, object>
    {
        private readonly ICacheService _cacheService;
        private readonly IAspNetUserSiteRepository _userSiteRepository;
        private readonly IAspNetUserSessionRepository _userSessionRepository;
        
        private readonly ezSQLDBContext _dbcontext;
        private List<AspNetUserSession> userSessionDeletes = new List<AspNetUserSession>();
        private List<string> cacheTokenDeletes = new List<string>();
        private AspNetUserSite? userSite;

        public AccountDeleteServices(
            ezSQLDBContext dbContext,
            IAspNetUserSiteRepository userSiteRepository,
            IAspNetUserSessionRepository userSessionRepository,
            ICacheService cacheService,
            SlackSendMessageServices slackSendMessageServices,
            IHttpContextAccessor httpContextAccessor,
            string successMessageDefault = AccountConfigKeywords.DELETE_ACCOUNT_SUCCESS)
            : base(slackSendMessageServices, httpContextAccessor, successMessageDefault)
        {
            _userSessionRepository = userSessionRepository;
            _userSiteRepository = userSiteRepository;
            _cacheService = cacheService;
            _dbcontext = dbContext;

        }

        public override void P1GenerateObjects()
        {
            userSite = _userSiteRepository.GetUserSiteDetail(_dataRequest.UserID, SiteID);
            if (userSite == null)
            {
                throw new BaseExceptionResult { Messages = AccountConfigKeywords.USER_SITE_NOT_EXIST };
            }
            if (userSite.Status == UserSiteStatus.DELETED)
            {
                throw new BaseExceptionResult { Messages = AccountConfigKeywords.USER_SITE_HAS_DELETED };
            }
            userSessionDeletes = _userSessionRepository.SessionsUserSites(_dataRequest.UserID, SiteID).ToList();
            cacheTokenDeletes = userSessionDeletes.Select(s => s.AuthToken).ToList();
            
        }

        public override void P2PostValidation()
        {

        }

        public override void P3AccessDatabase()
        {
            if (userSessionDeletes.Count > 0)
            {
                _userSessionRepository.DeleteRange(userSessionDeletes);
            }
            userSite.Status = UserSiteStatus.DELETED;
            userSite.LastModifiedByUserId = UserIDStr;

            _dbcontext.SaveChanges();
            foreach (string token in cacheTokenDeletes)
            {
                string hashKey = _cacheService.GetHashKey(RedisCacheKeys.USER_SESSION, token);
                if (_cacheService.Any(RedisCacheKeys.USER_SESSION, hashKey))
                {
                    _cacheService.Remove(RedisCacheKeys.USER_SESSION, hashKey);
                }
            }
        }

        public override void P4GenerateResponseData()
        {

        }
    }
}