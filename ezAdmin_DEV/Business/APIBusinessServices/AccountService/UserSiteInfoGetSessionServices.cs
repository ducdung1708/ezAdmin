using Business.APIBusinessServices.ThirtyPartyApp;
using Infrastructure.ConstantsDefine.HardCode;
using Models.Models.Response;
using Repository.Interfaces;

namespace Business.APIBusinessServices.Account
{
    public class UserSiteInfoGetSessionServices : BaseBusinessServices<object, SessionUserSiteInfoResponse>
    {
        private readonly IAspNetUserSiteRepository _aspNetUserSiteRepository;
        private readonly ICompanyRepository _companyRepository;

        private string? sessionStatus = SessionStatus.ALIVE;
        private string? userSiteStatusStatus;

        public UserSiteInfoGetSessionServices(
            IAspNetUserSiteRepository aspNetUserSiteRepository,
            ICompanyRepository companyRepository,
            SlackSendMessageServices slackSendMessageServices,
            IHttpContextAccessor httpContextAccessor, 
            string successMessageDefault = "Account Session OK") 
            : base(slackSendMessageServices, httpContextAccessor, successMessageDefault)
        {
            _aspNetUserSiteRepository = aspNetUserSiteRepository;
            _companyRepository = companyRepository;   
        }

        public override void P1GenerateObjects()
        {
            if (IsSystemAdmin || IsSystemSupport)
            {
                userSiteStatusStatus = SiteID.HasValue ? UserSiteStatus.ACTIVE : null;
            }
            else if (SiteID.HasValue)
            {
                var siteDetail = _companyRepository.GetById<Guid?>(SiteID);
                if (siteDetail != null)
                {
                    if (siteDetail.Status == SiteStatus.DEACTIVE)
                    {
                        sessionStatus = SessionStatus.EXPIRED;
                    }
                    var userSiteDetail = _aspNetUserSiteRepository.GetUserSiteDetail(UserIDStr, SiteID);
                    if (userSiteDetail != null)
                    {
                        userSiteStatusStatus = userSiteDetail.Status;
                        if (userSiteDetail.Status == UserSiteStatus.DELETED || userSiteDetail.Status == UserSiteStatus.DEACTIVE)
                        {
                            sessionStatus = SessionStatus.EXPIRED;
                        }
                        else if (userSiteDetail.ExpireDate.HasValue && userSiteDetail.ExpireDate.Value.AddMinutes(1439) < DateTime.UtcNow)
                        {
                            sessionStatus = SessionStatus.EXPIRED;
                        }
                    }
                    else
                    {
                        sessionStatus = SessionStatus.EXPIRED;
                    }
                }
                else
                {
                    sessionStatus = SessionStatus.EXPIRED;
                }
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
            _dataResponse = new SessionUserSiteInfoResponse
            {
                SessionStatus = sessionStatus,
                UserSiteStatus = userSiteStatusStatus,
            };
        }
    }
}
