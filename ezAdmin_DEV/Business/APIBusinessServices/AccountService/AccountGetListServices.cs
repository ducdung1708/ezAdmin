using Business.APIBusinessServices.ThirtyPartyApp;
using Infrastructure.ConstantsDefine.KeywordTranslate;
using Models.Models.Response;
using Repository.Interfaces;

namespace Business.APIBusinessServices.Account
{
    public class AccountGetListServices : BaseBusinessServices<Guid?, List<AccountGetListResponse>>
    {
        private readonly IAspNetUserRepository _userRepository;

        public AccountGetListServices(
            IAspNetUserRepository userRepository,
            SlackSendMessageServices slackSendMessageServices,
            IHttpContextAccessor httpContextAccessor,
            string successMessageDefault = CommonKeywords.GET_DATA_SUCESS) 
            : base(slackSendMessageServices, httpContextAccessor, successMessageDefault)
        {
            _userRepository = userRepository;
        }

        public override void P1GenerateObjects()
        {
            if (!_dataRequest.HasValue)
            {
                _dataRequest = SiteID;
            }
        }

        public override void P2PostValidation()
        {

        }

        public override void P3AccessDatabase()
        {
            _dataResponse = _userRepository.GetUserBySite(_dataRequest);
        }

        public override void P4GenerateResponseData()
        {
        }
    }
}