using Business.APIBusinessServices.ThirtyPartyApp;
using Business.ezID;
using Infrastructure.ConstantsDefine.HardCode;
using Infrastructure.ConstantsDefine.KeywordTranslate;
using Infrastructure.Core.Email;
using Models.Models.Request;
using Models.Models.Result;
using Models.ThirdParty.ezID.Request;
using Repository.Interfaces;

namespace Business.APIBusinessServices.Account
{
    public class UserRoleChangeGroupServices : BaseBusinessServices<AccountChangeRoleGroupRequest, object>
    {
        private readonly IAspNetUserRepository _userRepository;
        private readonly IAspNetGroupUserRepository _groupUserRepository;
        private readonly IAspNetUserSiteRepository _userSiteRepository;
        private readonly ezIDAPIServices _ezIDAPIServices;
        private readonly EmailService _emailService;
        private List<string> groupCodeNotAllowUpdate = new List<string> { GroupUserCodes.SYSTEM_ADMIN, GroupUserCodes.SYSTEM_SUPPORT };

        public UserRoleChangeGroupServices(
            IAspNetUserRepository userRepository,
            IAspNetGroupUserRepository groupUserRepository,
            IAspNetUserSiteRepository userSiteRepository,
            ezIDAPIServices ezIDAPIServices,
            EmailService emailService,
            SlackSendMessageServices slackSendMessageServices,
            IHttpContextAccessor httpContextAccessor, 
            string successMessageDefault = CommonKeywords.UPDATE_SUCCESS) 
            : base(slackSendMessageServices, httpContextAccessor, successMessageDefault)
        {
            _userRepository = userRepository;
            _groupUserRepository = groupUserRepository;
            _userSiteRepository = userSiteRepository;
            _ezIDAPIServices = ezIDAPIServices;
            _emailService = emailService;
        }

        public override void P1GenerateObjects()
        {
            var userActionDetail = _userRepository.GetById<string>(UserIDStr);
            var tokenRequestResult = _ezIDAPIServices.Token(new TokenRequest
            {
                username = userActionDetail.Email,
                password = _dataRequest.Password
            });
            if (tokenRequestResult.StatusCode != StatusCodes.Status200OK.ToString())
            {
                throw new BaseExceptionResult { Messages = AccountConfigKeywords.PASSWORD_INCORRECT };
            }
            UserSiteRoleNameResult userSiteRoleName = _userRepository.GetUserSiteRoleName(UserIDStr, SiteID);
            if (!groupCodeNotAllowUpdate.Contains(userSiteRoleName.GroupUserCode) && userSiteRoleName.GroupUserCode != GroupUserCodes.OWNER)
            {
                throw new BaseExceptionResult { Messages = CommonKeywords.ONLY_OWNER_IS_ALLOWED };
            }
            var userDetail = _userRepository.GetById<string>(_dataRequest.UserID);
            if (userDetail == null)
            {
                throw new BaseExceptionResult { Messages = CommonKeywords.USER_NOT_EXIST };
            }
            var userSiteDetail = _userSiteRepository.GetUserSiteDetail(_dataRequest.UserID, SiteID);
            if (userSiteDetail == null)
            {
                throw new BaseExceptionResult { Messages = AccountConfigKeywords.USER_SITE_NOT_EXIST };
            }
            if (userSiteDetail.Status == UserSiteStatus.DELETED)
            {
                throw new BaseExceptionResult { Messages = AccountConfigKeywords.USER_SITE_HAS_DELETED };
            }
            if (userSiteDetail.Status == UserSiteStatus.WAITTING_ACCEPT_INVITE)
            {
                throw new BaseExceptionResult { Messages = AccountConfigKeywords.ACCOUNT_WAITTING_CONFIRM_PLEASE_WAIT };
            }
            var groupUserSiteOldDetail = _groupUserRepository
                .GetBy(s => s.AspNetGroupUserId == userSiteDetail.AspNetGroupUserId)
                .FirstOrDefault();
            if (groupUserSiteOldDetail == null)
            {
                throw new BaseExceptionResult { Messages = CommonKeywords.GROUP_USER_NOT_EXIST };
            }
            if (groupCodeNotAllowUpdate.Contains(groupUserSiteOldDetail.GroupUserCode))
            {
                throw new BaseExceptionResult { Messages = CommonKeywords.GROUP_USER_INVALID };
            }
            var groupUserSiteUpdateDetail = _groupUserRepository
                .GetBy(s => s.AspNetGroupUserId == _dataRequest.GroupUserID && s.SiteId == SiteID)
                .FirstOrDefault();
            if (groupUserSiteUpdateDetail == null)
            {
                throw new BaseExceptionResult { Messages = CommonKeywords.GROUP_USER_SITE_NOT_EXIST };
            }
            if (groupCodeNotAllowUpdate.Contains(groupUserSiteUpdateDetail.GroupUserCode))
            {
                throw new BaseExceptionResult { Messages = CommonKeywords.GROUP_USER_INVALID };
            }
        }

        public override void P2PostValidation()
        {
        }

        public override void P3AccessDatabase()
        {
        }

        public override void P4GenerateResponseData()
        {
        }
    }
}
