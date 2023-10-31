using Business.APIBusinessServices.ThirtyPartyApp;
using Infrastructure.ConstantsDefine.HardCode;
using Infrastructure.ConstantsDefine.KeywordTranslate;
using Models.DBContext;
using Models.EntityModels;
using Models.Models.Request;
using Models.Models.Result;
using Repository.Interfaces;

namespace Business.APIBusinessServices.Account
{
    public class AccountUpdateService : BaseBusinessServices<AccountUpdateRequest, object>
    {
        private readonly ezSQLDBContext _dbcontext;
        private readonly IAspNetGroupUserRepository _groupUserRepository;
        private readonly IAspNetUserSiteRepository _userSiteRepository;
        private AspNetUserSite? userSiteUpdate;

        public AccountUpdateService(
            ezSQLDBContext dbcontext,
            IAspNetUserSiteRepository userSiteRepository,
            IAspNetGroupUserRepository groupUserRepository,
            SlackSendMessageServices slackSendMessageServices,
            IHttpContextAccessor httpContextAccessor,
            string successMessageDefault = CommonKeywords.UPDATE_SUCCESS)
            : base(slackSendMessageServices, httpContextAccessor, successMessageDefault)
        {
            _dbcontext = dbcontext;
            _userSiteRepository = userSiteRepository;
            _groupUserRepository = groupUserRepository;
        }

        public override void P1GenerateObjects()
        {
            userSiteUpdate = _userSiteRepository.GetUserSiteDetail(_dataRequest.UserID, SiteID);
            if (userSiteUpdate == null)
            {
                throw new BaseExceptionResult { Messages = AccountConfigKeywords.USER_SITE_NOT_EXIST };
            }
            var groupUserOldDetail = _groupUserRepository.GetById<Guid>(userSiteUpdate.AspNetGroupUserId);
            if (groupUserOldDetail == null)
            {
                throw new BaseExceptionResult { Messages = AccountConfigKeywords.GROUP_USER_SITE_NOT_EXIST };
            }
            if (groupUserOldDetail.GroupUserCode == GroupUserCodes.SYSTEM_ADMIN || groupUserOldDetail.GroupUserCode == GroupUserCodes.SYSTEM_SUPPORT)
            {
                throw new BaseExceptionResult { Messages = CommonKeywords.USER_INVALID};
            }
            if (_dataRequest.GroupUserID != groupUserOldDetail.AspNetGroupUserId)
            {
                var groupUserUpdateDetail = _groupUserRepository.GetById<Guid?>(_dataRequest.GroupUserID);
                if (groupUserUpdateDetail == null)
                {
                    throw new BaseExceptionResult { Messages = AccountConfigKeywords.GROUP_USER_SITE_NOT_EXIST };
                }
                if (groupUserUpdateDetail.GroupUserCode == GroupUserCodes.SYSTEM_ADMIN || groupUserUpdateDetail.GroupUserCode == GroupUserCodes.SYSTEM_SUPPORT)
                {
                    throw new BaseExceptionResult { Messages = CommonKeywords.GROUP_USER_INVALID };
                }
            }
        }

        public override void P2PostValidation()
        {

        }

        public override void P3AccessDatabase()
        {
            userSiteUpdate.AspNetGroupUserId = _dataRequest.GroupUserID.Value;
            userSiteUpdate.ExpireDate = _dataRequest.ExpireDate;
            _dbcontext.SaveChanges();
        }

        public override void P4GenerateResponseData()
        {

        }
    }
}