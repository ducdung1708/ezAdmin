using System;
using Business.APIBusinessServices.ThirtyPartyApp;
using Infrastructure.ConstantsDefine.HardCode;
using Infrastructure.ConstantsDefine.KeywordTranslate;
using Models.DBContext;
using Models.EntityModels;
using Models.Models.Request;
using Models.Models.Response;
using Models.Models.Result;
using Repository.Implementation;
using Repository.Interfaces;

namespace Business.APIBusinessServices.CountryServices
{
    public class CountryCreateService : BaseBusinessServices<CountryCreateRequest, CountryCreateResponse>
    {
        
        private readonly ICountryRepository _countryRepository;
        private Country? newCountry;
        private int newCountryId;


        public CountryCreateService(        
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
            //get max id
            CountryDetailResult? countryDetailResult = _countryRepository.GetMaxIdDetail();
            newCountryId = countryDetailResult is null ? 0 : countryDetailResult.Id;
            newCountryId++;
            newCountry = new Country
            {
                Id = newCountryId,
                Name = _dataRequest.Name?? "",
                Code = _dataRequest.Code?? "",
                Description = _dataRequest.Description,
                CurrencyName = _dataRequest.CurrencyName,
                CurrencyFormat = _dataRequest.CurrencyFormat,
                DecimalSeparator = _dataRequest.DecimalSeparator,
                Symbol = _dataRequest.Symbol                
            };
        }

        public override void P2PostValidation()
        {
            if ((_dataRequest is null) || (_dataRequest.Name is null) || (_dataRequest.Name==""))
            {
                throw new BaseExceptionResult { Messages = ValidationKeywords.VALIDATION_FAIL };
            }
            if ((_dataRequest is null) || (_dataRequest.Code is null) || (_dataRequest.Code == ""))
            {
                throw new BaseExceptionResult { Messages = ValidationKeywords.VALIDATION_FAIL };
            }
        }

        public override void P3AccessDatabase()
        {
            _countryRepository.Add(newCountry);
            _countryRepository.Save();

        }

        public override void P4GenerateResponseData()
        {
            _dataResponse = new CountryCreateResponse
            {
                Id = newCountryId
            };
        }
    }
}

