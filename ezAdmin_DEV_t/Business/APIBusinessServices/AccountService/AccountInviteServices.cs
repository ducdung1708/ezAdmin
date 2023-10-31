using Business.APIBusinessServices.ThirtyPartyApp;
using Business.ezID;
using Infrastructure.ConstantsDefine.HardCode;
using Infrastructure.ConstantsDefine.KeywordTranslate;
using Models.DBContext;
using Models.EntityModels;
using Models.Models.Request;
using Models.Models.Result;
using Models.ThirdParty.ezID.Request;
using Repository.Interfaces;

namespace Business.APIBusinessServices.Account
{
    public class AccountInviteServices : BaseBusinessServices<AccountInviteRequest, object>
    {
        private readonly ezSQLDBContext _context;
        private readonly ezIDAPIServices _ezIDAPIServices;
        private readonly IAspNetUserRepository _aspNetUserRepository;
        private readonly IAspNetGroupUserRepository _groupUserRepository;
        private readonly IAspNetUserSiteRepository _aspNetUserSiteRepository;
        private List<string> userSiteStatusValidAdd = new List<string> { UserSiteStatus.ACTIVE, UserSiteStatus.DEACTIVE, UserSiteStatus.WAITTING_ACCEPT_INVITE };
        private AspNetUserSite? newUserSite;
        private AspNetUserSite? userSiteDetail;
        private AspNetUser? newUser;
        private Guid groupUserID;

        public AccountInviteServices(
            ezSQLDBContext context,
            ezIDAPIServices ezIDAPIServices,
            IAspNetUserRepository aspNetUserRepository,
            IAspNetGroupUserRepository groupUserRepository,
            IAspNetUserSiteRepository aspNetUserSiteRepository,
            SlackSendMessageServices slackSendMessageServices,
            IHttpContextAccessor httpContextAccessor, 
            string successMessageDefault = AccountConfigKeywords.INVITE_ACCOUNT_JOIN_SITE_SUCCESS) 
            : base(slackSendMessageServices, httpContextAccessor, successMessageDefault)
        {
            _context = context;
            _ezIDAPIServices = ezIDAPIServices;
            _aspNetUserRepository = aspNetUserRepository;
            _groupUserRepository = groupUserRepository;
            _aspNetUserSiteRepository = aspNetUserSiteRepository;
        }

        public override void P1GenerateObjects()
        {
            groupUserID = _dataRequest.GroupUserID.Value;
            _dataRequest.Email = (_dataRequest.Email ?? "").ToLower();
            var groupUserDetail = _groupUserRepository.GetBy(s => s.AspNetGroupUserId == groupUserID && s.SiteId == SiteID).FirstOrDefault();
            if (groupUserDetail == null)
            {
                throw new BaseExceptionResult { Messages = CommonKeywords.GROUP_USER_SITE_NOT_EXIST };
            }
            if (groupUserDetail.GroupUserCode == GroupUserCodes.SYSTEM_ADMIN || groupUserDetail.GroupUserCode == GroupUserCodes.SYSTEM_SUPPORT)
            {
                throw new BaseExceptionResult { Messages = CommonKeywords.GROUP_USER_INVALID };
            }
            var userDetail = _aspNetUserRepository.GetBy(s => s.Email.ToLower() == _dataRequest.Email).FirstOrDefault();
            if (userDetail != null)
            {
                UserSiteRoleNameResult userSiteRoleName = _aspNetUserRepository.GetUserSiteRoleName(userDetail.Id, SiteID);
                if (userSiteRoleName != null)
                {
                    string roleName = userSiteRoleName.GroupUserCode;
                    if (roleName == GroupUserCodes.SYSTEM_ADMIN || roleName == GroupUserCodes.SYSTEM_SUPPORT)
                    {
                        throw new BaseExceptionResult { Messages = AccountConfigKeywords.EMAIL_HAS_ACCESS_SITE };
                    }
                }
                userSiteDetail = _aspNetUserSiteRepository.GetUserSiteDetail(userDetail.Id, SiteID);
                if (userSiteDetail != null)
                {
                    if (userSiteStatusValidAdd.Contains(userSiteDetail.Status))
                    {
                        throw new BaseExceptionResult { Messages = AccountConfigKeywords.EMAIL_HAS_ACCESS_SITE };
                    }
                }
                else
                {
                    newUserSite = new AspNetUserSite
                    {
                        AspNetUserSiteId = Guid.NewGuid(),
                        AspNetGroupUserId = groupUserID,
                        Status = UserSiteStatus.WAITTING_ACCEPT_INVITE,
                        SiteId = SiteID,
                        UserId = userDetail.Id,
                        InviteByUserId = UserIDStr,
                        IntiveDate = DateTime.UtcNow
                    };
                }
            }
            else
            {
                Guid newUserID = Guid.NewGuid();
                newUser = new AspNetUser
                {
                    Id = newUserID.ToString(),
                    Email = _dataRequest.Email,
                    UserName = _dataRequest.Email,
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    FullName = _dataRequest.Email,
                    LanguageCode = LocaleLanguageCodes.VI,
                    IsActive = true,
                };
                newUserSite = new AspNetUserSite
                {
                    AspNetUserSiteId = Guid.NewGuid(),
                    AspNetGroupUserId = groupUserID,
                    Status = UserSiteStatus.WAITTING_ACCEPT_INVITE,
                    SiteId = SiteID,
                    UserId = newUserID.ToString(),
                    InviteByUserId = UserIDStr,
                    IntiveDate = DateTime.UtcNow
                };
            }
            var createUserezIDResponse =_ezIDAPIServices.CreateUser(new UserCreateRequest
            {
                email = _dataRequest.Email
            });
            if (createUserezIDResponse.StatusCode != StatusCodes.Status200OK.ToString())
            {
                throw new BaseExceptionResult { Messages = CommonKeywords.PROCESS_DATA_ERROR };
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
            if (newUserSite != null)
            {
                _aspNetUserSiteRepository.Add(newUserSite);
            }
            else if (userSiteDetail != null)
            {
                userSiteDetail.Status = UserSiteStatus.WAITTING_ACCEPT_INVITE;
                userSiteDetail.ExpireDate = null;
                userSiteDetail.IntiveDate = DateTime.UtcNow;
                userSiteDetail.InviteByUserId = UserIDStr;
                userSiteDetail.AspNetGroupUserId = groupUserID;
                userSiteDetail.LastModifiedDate = DateTime.UtcNow;
                userSiteDetail.LastModifiedByUserId = UserIDStr;
            }
            _context.SaveChanges();
        }

        public override void P4GenerateResponseData()
        {
        }
    }
}
