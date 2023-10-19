using Business.APIBusinessServices.ThirtyPartyApp;
using Infrastructure.ConstantsDefine.HardCode;
using Infrastructure.ConstantsDefine.KeywordTranslate;
using Models.EntityModels;
using Models.Models.Request;
using Models.Models.Response;
using Models.Models.Result;
using Repository.Implementation;
using Repository.Interfaces;

namespace Business.APIBusinessServices.CompanyService
{
	public class CompanyCreateService : BaseBusinessServices<CompanyCreateRequest, CompanyCreateResponse>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ICountryRepository _countryRepository;
        private Company? newCompany;
        private Guid newSiteID = Guid.NewGuid();


        public CompanyCreateService(
            ICompanyRepository companyRepository,
            ICountryRepository countryRepository,
            SlackSendMessageServices slackSendMessageServices,
            IHttpContextAccessor httpContextAccessor,
            string successMessageDefault = ""            
        ) : base(slackSendMessageServices, httpContextAccessor, successMessageDefault)
        {
            _companyRepository = companyRepository;
            _countryRepository = countryRepository;
        }

        public override void P1GenerateObjects()
        {
            newCompany = new Company
            {
                Id = newSiteID,
                Name = _dataRequest.Name,
                Address = _dataRequest.Address,
                Email = _dataRequest.Email,
                Phone = _dataRequest.Phone,
                TaxCode = _dataRequest.TaxCode,
                Partner = _dataRequest.Partner,
                PartnerUrl = _dataRequest.PartnerUrl,
                Username = _dataRequest.Username,
                Password = _dataRequest.Password,
                PartnerUrl2 = _dataRequest.PartnerUrl2,
                Username2 = _dataRequest.Username2,
                Password2 = _dataRequest.Password2,
                CountryCode = _dataRequest.CountryCode,
                Avatar = _dataRequest.Avatar,
                Status = _dataRequest.Status,
                PartnerConnectStatus = SiteConnectPartnerStatus.NOT_CONNECT
            };
        }

        public override void P2PostValidation()
        {
            if (_dataRequest.CountryCode == null)
            {
                throw new BaseExceptionResult { Messages = SiteKeywords.COUNTRY_NOT_EXIST };
            }
            
            Country? country = _countryRepository.GetBy(s => s.Code == newCompany.CountryCode).FirstOrDefault();
            if (country == null)
            {
                throw new BaseExceptionResult { Messages = SiteKeywords.COUNTRY_NOT_EXIST };
            }

        }

        public override void P3AccessDatabase()
        {

            _companyRepository.Add(newCompany);
            _companyRepository.Save();
        }

        public override void P4GenerateResponseData()
        {
            _dataResponse = new CompanyCreateResponse
            {
                Id = newSiteID
            };
        }
    }
}

