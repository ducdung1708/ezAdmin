using System;
using System.Linq.Expressions;
using Business.APIBusinessServices.ThirtyPartyApp;
using Infrastructure.ConstantsDefine.KeywordTranslate;
using Models.EntityModels;
using Models.Models.Others;
using Models.Models.Request;
using Models.Models.Response;
using Models.Models.Result;
using Repository.Implementation;
using Repository.Interfaces;

namespace Business.APIBusinessServices.CountryServices
{
    public class CountryGetListService : BaseBusinessServices<CountryGetListRequest, List<CountryGetListResponse>>
    {
        private readonly ICountryRepository _countryRepository;        
        private List<CountryGetListResponse> _listCountryQuery;

        public CountryGetListService(
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
            _dataRequest.NameOrCode = _dataRequest.NameOrCode ?? "";            
            _dataRequest.PageIndex = _dataRequest.PageIndex ?? 1;
            _dataRequest.PageSize = _dataRequest.PageSize ?? 20;
        }

        public override void P2PostValidation()
        {

            if (_dataRequest.PageIndex < 0 || _dataRequest.PageSize < 0)
            {
                throw new BaseExceptionResult { Messages = ValidationKeywords.VALIDATION_FAIL };
            }
        }

        public override void P3AccessDatabase()
        {
            int skipRows = _dataRequest.PageSize.Value * (_dataRequest.PageIndex.Value - 1);

            Expression<Func<Country, bool>> companyConditionSearch = s => (s.Name ?? "").Contains(_dataRequest.NameOrCode) || (s.Code ?? "").Contains(_dataRequest.NameOrCode);

            _listCountryQuery = _countryRepository
                .GetBy(companyConditionSearch)
                .Select(s => new CountryGetListResponse
                {
                    Id = s.Id,
                    Name = s.Name,
                    Code = s.Code,
                    CurrencyName = s.CurrencyName,
                    CurrencyFormat = s.CurrencyFormat,
                    Description = s.Description,
                    DecimalSeparator = s.DecimalSeparator,
                    Symbol = s.Symbol
                })
                .Skip(skipRows)
                .Take(_dataRequest.PageSize.Value)
                .ToList();
        }

        public override void P4GenerateResponseData()
        {
            _dataResponse = _listCountryQuery;
        }
    }
}

