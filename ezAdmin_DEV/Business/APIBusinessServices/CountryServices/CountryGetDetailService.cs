using System;
using Business.APIBusinessServices.ThirtyPartyApp;
using Infrastructure.ConstantsDefine.KeywordTranslate;
using Models.Models.Response;
using Models.Models.Result;
using Repository.Implementation;
using Repository.Interfaces;

namespace Business.APIBusinessServices.CountryServices
{
    public class CountryGetDetailService : BaseBusinessServices<int, CountryGetDetailResponse>
    {

        private readonly ICountryRepository _countryRepository;
        private CountryDetailResult? _countryDetailResult;

        public CountryGetDetailService(
            ICountryRepository countryRepository,
            SlackSendMessageServices slackSendMessageServices,
            IHttpContextAccessor httpContextAccessor,
            string successMessageDefault = ""
        ) : base(slackSendMessageServices, httpContextAccessor, successMessageDefault)
        {
            _countryRepository = countryRepository;
        }

        public override void P1GenerateObjects()
        {
            
        }

        public override void P2PostValidation()
        {
            
        }

        public override void P3AccessDatabase()
        {
            _countryDetailResult = _countryRepository.GetCountryDetail(_dataRequest);

            if (_countryDetailResult == null)
            {
                throw new BaseExceptionResult
                {
                    StatusCode = StatusCodes.Status404NotFound.ToString(),
                    Messages = SiteKeywords.SITE_NOT_EXIST
                };
            }
        }

        public override void P4GenerateResponseData()
        {
            _dataResponse = new CountryGetDetailResponse
            {
                Id = _countryDetailResult.Id,
                Name = _countryDetailResult.Name,
                Code = _countryDetailResult.Code,
                Description = _countryDetailResult.Description,
                CurrencyName = _countryDetailResult.CurrencyName,
                CurrencyFormat = _countryDetailResult.CurrencyFormat,
                DecimalSeparator = _countryDetailResult.DecimalSeparator,
                Symbol = _countryDetailResult.Symbol
            };
        }
    }
}

