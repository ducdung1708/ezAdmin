using System;
using System.Linq.Expressions;
using Business.APIBusinessServices.ThirtyPartyApp;
using Infrastructure.ConstantsDefine.KeywordTranslate;
using Models.EntityModels;
using Models.Models.Request;
using Models.Models.Response;
using Models.Models.Result;
using Repository.Implementation;
using Repository.Interfaces;

namespace Business.APIBusinessServices.CountryServices
{
    public class CountryDeleteService : BaseBusinessServices<int, CountryDeleteResponse>
    {
        private readonly ICountryRepository _countryRepository;
        private readonly ICompanyRepository _companyRepository;
        private int nr;


        public CountryDeleteService(
            ICountryRepository countryRepository,
            ICompanyRepository companyRepository,
            SlackSendMessageServices slackSendMessageServices,
            IHttpContextAccessor httpContextAccessor,
            string successMessageDefault = ""
        ) : base(slackSendMessageServices, httpContextAccessor, successMessageDefault)
        {
            _countryRepository = countryRepository;
            _companyRepository = companyRepository;
        }

        public override void P1GenerateObjects()
        {
            
        }

        public override void P2PostValidation()
        {
            if (_dataRequest == null)
            {
                throw new BaseExceptionResult { Messages = ValidationKeywords.VALIDATION_FAIL };
            }

            //Can not delete if exist an Company

            //Get countryCode
            Country country = _countryRepository.GetById(_dataRequest);

            if ((country == null) || (country.Code == null))
            {
                return;
            }

            //Get a company by countryCode
            CompanyGetListResponse company = _companyRepository
                .GetBy(s => (s.CountryCode ?? "").Equals(country.Code))
                .Select(s => new CompanyGetListResponse
                {
                    Id = s.Id,
                    Address = s.Address,
                    Name = s.Name,
                    Email = s.Email

                })
                .FirstOrDefault();

            if (company != null) //If exist a company in this country, not delete
            {
                throw new BaseExceptionResult { Messages = ValidationKeywords.VALIDATION_FAIL };
            }

        }

        public override void P3AccessDatabase()
        {
            _countryRepository.Delete<int>(_dataRequest);
            nr = _countryRepository.Save();

        }

        public override void P4GenerateResponseData()
        {
            _dataResponse = new CountryDeleteResponse
            {
                Id = _dataRequest,
                NumberOfRows = nr
            };

        }
    }
}

