using Business.APIBusinessServices.ThirtyPartyApp;
using Infrastructure.ConstantsDefine.KeywordTranslate;
using Models.EntityModels;
using Models.Models.Request;
using Models.Models.Response;
using Models.Models.Result;
using Repository.Interfaces;

namespace Business.APIBusinessServices.CityService
{
    public class CityUpdateService : BaseBusinessServices<CityUpdateRequest, CityUpdateResponse>
    {
        private readonly ICityRepository _cityRepository;
        private readonly ICountryRepository _countryRepository;
        private City updatedCity;
        private Guid newSiteID = Guid.NewGuid();
        private bool checkCountryUpdate = false;
        private int nr = 0;

        public CityUpdateService (
            ICityRepository cityRepository,
            ICountryRepository countryRepository,
            SlackSendMessageServices slackSendMessageServices,
            IHttpContextAccessor httpContextAccessor,
            string successMessageDefault = ""
        ) : base(slackSendMessageServices, httpContextAccessor, successMessageDefault)
        {
            _cityRepository = cityRepository;
            _countryRepository = countryRepository;
        }

        public override void P1GenerateObjects()
        {
            updatedCity = _cityRepository.GetById<Guid?>(_dataRequest.Id);
            if (updatedCity == null)
            {
                throw new BaseExceptionResult { Messages = SiteKeywords.SITE_NOT_EXIST };
            }

            checkCountryUpdate = !string.IsNullOrEmpty(_dataRequest.CountryId.ToString()) && _dataRequest.CountryId != updatedCity.CountryId;
            //Console.WriteLine(_dataRequest.CountryId, updatedCity.CountryId);
            updatedCity.Name = _dataRequest.Name ?? updatedCity.Name;
            updatedCity.Code = _dataRequest.Code ?? updatedCity.Code;
            updatedCity.AirportCode = _dataRequest.AirportCode ?? updatedCity.AirportCode;
            updatedCity.PhoneCode = _dataRequest.PhoneCode ?? updatedCity.PhoneCode;
            updatedCity.ExtraCode = _dataRequest.ExtraCode ?? updatedCity.ExtraCode;
            updatedCity.PostalCode = _dataRequest.PostalCode ?? updatedCity.PostalCode;
            updatedCity.Latitude = _dataRequest.Latitude ?? updatedCity.Latitude;
            updatedCity.Longtitude = _dataRequest.Longtitude ?? updatedCity.Longtitude;
            updatedCity.SortIndex = _dataRequest.SortIndex ?? updatedCity.SortIndex;
            updatedCity.Active = _dataRequest.Active ?? updatedCity.Active;
            updatedCity.CreatedDate = _dataRequest.CreatedDate ?? updatedCity.CreatedDate;
            updatedCity.CreatedBy = _dataRequest.CreatedBy ?? updatedCity.CreatedBy;
            updatedCity.UpdatedDate = _dataRequest.UpdatedDate ?? updatedCity.UpdatedDate;
            updatedCity.UpdatedBy = _dataRequest.UpdatedBy ?? updatedCity.UpdatedBy;
            updatedCity.CountryId = _dataRequest.CountryId ?? updatedCity.CountryId;
        }

        public override void P2PostValidation()
        {
            if (checkCountryUpdate)
            {
                Country? country = _countryRepository
                    .GetBy(s => s.Id == updatedCity.CountryId)
                    .FirstOrDefault();

                if (country == null)
                {
                    throw new BaseExceptionResult { Messages = SiteKeywords.COUNTRY_NOT_EXIST };
                }
            }

        }

        public override void P3AccessDatabase()
        {
            if (updatedCity != null)
            {
                //build query
                _cityRepository.Update(updatedCity);

                //execute query
                nr = _cityRepository.Save();
            }
        }

        public override void P4GenerateResponseData()
        {
            _dataResponse = new CityUpdateResponse
            {
                Id = newSiteID,
                NumberOfRows = nr
            };
        }
    }
}

