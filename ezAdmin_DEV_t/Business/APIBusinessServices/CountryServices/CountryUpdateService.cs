using System;
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
    public class CountryUpdateService : BaseBusinessServices<CountryUpdateRequest, CountryUpdateResponse>
    {
        private readonly ICountryRepository _countryRepository;        
        private Country updatedCountry;        
        private int nr = 0;
        
        public CountryUpdateService(
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
            updatedCountry = _countryRepository.GetById<int>(_dataRequest.Id);
                
            if (updatedCountry == null)
            {
                throw new BaseExceptionResult { Messages = SiteKeywords.SITE_NOT_EXIST };
            }
            
            updatedCountry.Name = _dataRequest.Name ?? updatedCountry.Name;
            updatedCountry.Code = _dataRequest.Code ?? updatedCountry.Code;
            updatedCountry.Description = _dataRequest.Description ?? updatedCountry.Description;
            updatedCountry.CurrencyName = _dataRequest.CurrencyName ?? updatedCountry.CurrencyName;
            updatedCountry.CurrencyFormat = _dataRequest.CurrencyFormat ?? updatedCountry.CurrencyFormat;
            updatedCountry.DecimalSeparator = _dataRequest.DecimalSeparator;
            updatedCountry.Symbol = _dataRequest.Symbol;
        }

        public override void P2PostValidation()
        {
           
        }

        public override void P3AccessDatabase()
        {
            if (updatedCountry != null)
            {
                //build query
                _countryRepository.Update(updatedCountry);
                //execute query
                nr = _countryRepository.Save();
            }
        }

        public override void P4GenerateResponseData()
        {
            _dataResponse = new CountryUpdateResponse
            {
                Id = _dataRequest.Id,
                NumberOfRows = nr
            };
        }
    }
}

