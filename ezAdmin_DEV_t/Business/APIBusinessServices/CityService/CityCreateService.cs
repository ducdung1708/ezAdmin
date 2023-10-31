using Business.APIBusinessServices.ThirtyPartyApp;
using Infrastructure.ConstantsDefine.HardCode;
using Infrastructure.ConstantsDefine.KeywordTranslate;
using Models.EntityModels;
using Models.Models.Request;
using Models.Models.Response;
using Models.Models.Result;
using Repository.Implementation;
using Repository.Interfaces;
using Infrastructure.CheckValidInput;
namespace Business.APIBusinessServices.CityService
{
	public class CityCreateService : BaseBusinessServices<CityCreateRequest, CityCreateResponse>
    {
        private readonly ICityRepository _cityRepository;
        private readonly ICountryRepository _countryRepository;
        private City? newCity;
        private Guid newSiteID = Guid.NewGuid();


        public CityCreateService(
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
            newCity = new City
            {
                Id = newSiteID,
                Name = _dataRequest.Name,
                Code = _dataRequest.Code,
                AirportCode = _dataRequest.AirportCode,
                PhoneCode = _dataRequest.PhoneCode,
                ExtraCode = _dataRequest.ExtraCode,
                PostalCode = _dataRequest.PostalCode,
                Latitude = _dataRequest.Latitude,
                Longtitude = _dataRequest.Longtitude,
                SortIndex = _dataRequest.SortIndex,
                Active = _dataRequest.Active,
                CreatedDate = _dataRequest.CreatedDate,
                CreatedBy = _dataRequest.CreatedBy,
                UpdatedDate = _dataRequest.UpdatedDate,
                UpdatedBy = _dataRequest.UpdatedBy,
                CountryId = _dataRequest.CountryId
            };
        }

        public override void P2PostValidation()
        {
            // Check valid input.
            if (!CommonCheckValid.IsValidName(_dataRequest.Name))
            {
                throw new BaseExceptionResult { Messages = CommonKeywords.NAME_INVALID};
            }
            

            // Check country. 
            if (_dataRequest.CountryId == null)
            {
                throw new BaseExceptionResult { Messages = SiteKeywords.COUNTRY_NOT_EXIST };
            }

            Country? country = _countryRepository.GetBy(s => s.Id == newCity.CountryId).FirstOrDefault();
            if (country == null)
            {
                throw new BaseExceptionResult { Messages = SiteKeywords.COUNTRY_NOT_EXIST };
            }

        }

        public override void P3AccessDatabase()
        {

            _cityRepository.Add(newCity);
            _cityRepository.Save();
        }

        public override void P4GenerateResponseData()
        {
            _dataResponse = new CityCreateResponse
            {
                Id = newSiteID
            };
        }
    }
}

