using Business.APIBusinessServices.ThirtyPartyApp;
using Infrastructure.ConstantsDefine.KeywordTranslate;
using Models.Models.Response;
using Models.Models.Result;
using Repository.Interfaces;

namespace Business.APIBusinessServices.Resource
{
    public class GetResourceAppServices : BaseBusinessServices<object, ResourceAppResponse>
    {
        private readonly IAspNetUserRepository _aspNetUserRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IAspNetGroupUserRepository _aspNetGroupUserRepository;

        public GetResourceAppServices(
            IAspNetUserRepository aspNetUserRepository,
            ICompanyRepository companyRepository,
            ICityRepository cityRepository,
            IAspNetGroupUserRepository aspNetGroupUserRepository,
            SlackSendMessageServices slackSendMessageServices,
            IHttpContextAccessor httpContextAccessor, 
            string successMessageDefault = CommonKeywords.GET_DATA_SUCESS) 
            : base(slackSendMessageServices, httpContextAccessor, successMessageDefault)
        {
            _aspNetUserRepository = aspNetUserRepository;
            _companyRepository = companyRepository;
            _aspNetGroupUserRepository = aspNetGroupUserRepository;
            _cityRepository = cityRepository;
        }
        
        private List<MenuGroupUserSiteResourceResult> menuGroupUserSiteResults = new List<MenuGroupUserSiteResourceResult>();
        private List<UserSiteResourceResult> siteResourceResults = new List<UserSiteResourceResult>();
        private List<GroupUserSiteResult> groupUserSites = new List<GroupUserSiteResult>();

        public override void P1GenerateObjects()
        {
        }

        public override void P2PostValidation()
        {
        }

        public override void P3AccessDatabase()
        {
            menuGroupUserSiteResults = _aspNetUserRepository.GetUserMenusRolesResource(UserIDStr, SiteID);
            //siteResourceResults = _companyRepository.GetUserSiteResource(UserIDStr);
            if (SiteID.HasValue)
            {
                groupUserSites = _aspNetGroupUserRepository.GetGroupUsersSite(SiteID.Value)
                    .Select(s => new GroupUserSiteResult
                    {
                        GroupUserID = s.AspNetGroupUserID,
                        GroupUserCode = s.GroupUserCode,
                        Keyword = s.GroupUserKeyword,
                        Description = s.GroupUserDescription,
                        GroupRoleLevel = s.GroupLevel
                    })
                    .OrderBy(s => s.GroupRoleLevel)
                    .ToList();
            }
        }

        public override void P4GenerateResponseData()
        {
            _dataResponse = new ResourceAppResponse
            {
                MenusRoles = menuGroupUserSiteResults,
                Sites = siteResourceResults,
                GroupUsers = groupUserSites
            };
        }
    }
}
