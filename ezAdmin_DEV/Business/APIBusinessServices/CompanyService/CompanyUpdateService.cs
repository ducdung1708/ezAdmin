using Business.APIBusinessServices.ThirtyPartyApp;
using Infrastructure.ConstantsDefine.KeywordTranslate;
using Models.EntityModels;
using Models.Models.Request;
using Models.Models.Response;
using Models.Models.Result;
using Repository.Interfaces;

namespace Business.APIBusinessServices.CompanyService
{
    public class CompanyUpdateService : BaseBusinessServices<CompanyUpdateRequest, CompanyUpdateResponse>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ICountryRepository _countryRepository;
        private Company updatedCompany;
        private Guid newSiteID = Guid.NewGuid();
        private bool checkCountryUpdate = false;
        private int nr = 0;

        public CompanyUpdateService (
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
            updatedCompany = _companyRepository.GetById<Guid?>(_dataRequest.Id);
            if (updatedCompany == null)
            {
                throw new BaseExceptionResult { Messages = SiteKeywords.SITE_NOT_EXIST };
            }

            checkCountryUpdate = !string.IsNullOrEmpty(_dataRequest.CountryCode) && _dataRequest.CountryCode != updatedCompany.CountryCode;

            updatedCompany.Name = _dataRequest.Name ?? updatedCompany.Name;
            updatedCompany.Address = _dataRequest.Address ?? updatedCompany.Address;
            updatedCompany.Email = _dataRequest.Email ?? updatedCompany.Email;
            updatedCompany.Phone = _dataRequest.Phone ?? updatedCompany.Phone;
            updatedCompany.TaxCode = _dataRequest.TaxCode ?? updatedCompany.TaxCode;
            updatedCompany.Partner = _dataRequest.Partner ?? updatedCompany.Partner;
            updatedCompany.PartnerUrl = _dataRequest.PartnerUrl ?? updatedCompany.PartnerUrl;
            updatedCompany.Username = _dataRequest.Username ?? updatedCompany.Username;
            updatedCompany.Password = _dataRequest.Password ?? updatedCompany.Password;
            updatedCompany.PartnerUrl2 = _dataRequest.PartnerUrl2 ?? updatedCompany.PartnerUrl2;
            updatedCompany.Username2 = _dataRequest.Username2 ?? updatedCompany.Username2;
            updatedCompany.Password2 = _dataRequest.Password2 ?? updatedCompany.Password2;
            updatedCompany.CountryCode = _dataRequest.CountryCode ?? updatedCompany.CountryCode;
            updatedCompany.Avatar = _dataRequest.Avatar ?? updatedCompany.Avatar;
            updatedCompany.Status = _dataRequest.Status ?? updatedCompany.Status;
        }

        public override void P2PostValidation()
        {
            if (checkCountryUpdate)
            {
                Country? country = _countryRepository
                    .GetBy(s => s.Code == updatedCompany.CountryCode)
                    .FirstOrDefault();

                if (country == null)
                {
                    throw new BaseExceptionResult { Messages = SiteKeywords.COUNTRY_NOT_EXIST };
                }
            }

        }

        public override void P3AccessDatabase()
        {
            if (updatedCompany != null)
            {
                //build query
                _companyRepository.Update(updatedCompany);

                //execute query
                nr = _companyRepository.Save();
            }
        }

        public override void P4GenerateResponseData()
        {
            _dataResponse = new CompanyUpdateResponse
            {
                Id = newSiteID,
                NumberOfRows = nr
            };
        }
    }
}

