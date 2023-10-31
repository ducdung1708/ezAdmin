using Business.APIBusinessServices.ThirtyPartyApp;
using Infrastructure.ConstantsDefine;
using Infrastructure.ConstantsDefine.HardCode;
using Infrastructure.ConstantsDefine.KeywordTranslate;
using Infrastructure.Core.Cache;
using Models.DBContext;
using Models.EntityModels;
using Models.Models.Request;
using Models.Models.Response;
using Models.Models.Result;
using Repository.Interfaces;

namespace Business.APIBusinessServices.Account
{
    public class AccountUpdateStatusServices : BaseBusinessServices<AccountUpdateStatusRequest, object>
    {
        private readonly ezSQLDBContext _dbcontext;
        private readonly ICacheService _cacheService;
        private List<AspNetUserSession> userSessionDeletes = new List<AspNetUserSession>();
        private List<string> cacheTokenDeletes = new List<string>();
        private AspNetUserSite? userSiteUpdate;
        private readonly IAspNetUserSessionRepository _userSessionRepository;
        private readonly IAspNetUserSiteRepository _userSiteRepository;
        public AccountUpdateStatusServices(
            ICacheService cacheService,
            IAspNetUserSessionRepository userSessionRepository,
            IAspNetUserSiteRepository userSiteRepository,
            ezSQLDBContext dbContext,
            SlackSendMessageServices slackSendMessageServices,
            IHttpContextAccessor httpContextAccessor,
            string successMessageDefault = AccountConfigKeywords.UPDATE_ACCOUNT_STATUS_SUCCESS)
            : base(slackSendMessageServices, httpContextAccessor, successMessageDefault)
        {
            _userSessionRepository = userSessionRepository;
            _dbcontext = dbContext;
            _cacheService = cacheService;
            _userSiteRepository = userSiteRepository;
        }

        public override void P1GenerateObjects()
        {
            _dataRequest.ReasonDeactive = (_dataRequest.ReasonDeactive ?? "").Trim();
            if (_dataRequest.Status != UserSiteStatus.ACTIVE && _dataRequest.Status != UserSiteStatus.DEACTIVE)
            {
                throw new BaseExceptionResult { Messages = AccountConfigKeywords.STATUS_INCORRECT };
            }
            userSiteUpdate = _userSiteRepository.GetUserSiteDetail(_dataRequest.UserID, SiteID.Value);
            if (userSiteUpdate == null)
            {
                throw new BaseExceptionResult { Messages = AccountConfigKeywords.USER_SITE_NOT_EXIST };
            }
            if (userSiteUpdate.Status == _dataRequest.Status && _dataRequest.Status == UserSiteStatus.DEACTIVE)
            {
                throw new BaseExceptionResult { Messages = AccountConfigKeywords.USER_SITE_HAS_DEACTIVATED };
            }
            if (_dataRequest.Status == UserSiteStatus.DEACTIVE)
            {
                if (string.IsNullOrEmpty(_dataRequest.ReasonDeactive) || string.IsNullOrWhiteSpace(_dataRequest.ReasonDeactive))
                {
                    throw new BaseExceptionResult
                    {
                        StatusCode = StatusCodes.Status422UnprocessableEntity.ToString(),
                        Messages = ValidationKeywords.VALIDATION_FAIL,
                        MessagesDetails = new List<MessageResponseBase>
                        {
                            new MessageResponseBase("ReasonDeactive", ValidationKeywords.REASON_DEACTIVE_IS_REQUIRED)
                        }
                    };
                }
                userSessionDeletes = _userSessionRepository.GetBy(s => s.UserId == _dataRequest.UserID && s.SiteId == SiteID).ToList();
                cacheTokenDeletes = userSessionDeletes.Select(s => s.AuthToken).ToList();
            }
        }
        public override void P2PostValidation()
        {

        }
        public override void P3AccessDatabase()
        {
            if (userSessionDeletes.Count() > 0)
            {
                _userSessionRepository.DeleteRange(userSessionDeletes);
            }
            userSiteUpdate.Status = _dataRequest.Status;
            userSiteUpdate.LastModifiedByUserId = UserIDStr;
            _dbcontext.SaveChanges();
            foreach (string tokenDelete in cacheTokenDeletes)
            {
                string hashKeyUserSession = _cacheService.GetHashKey(RedisCacheKeys.USER_SESSION, tokenDelete);
                if (_cacheService.Any(RedisCacheKeys.USER_SESSION, hashKeyUserSession))
                {
                    _cacheService.Remove(RedisCacheKeys.USER_SESSION, hashKeyUserSession);
                }
            }
        }

        public override void P4GenerateResponseData()
        {

        }
    }
}