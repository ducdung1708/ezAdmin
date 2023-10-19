using Business.APIBusinessServices.ThirtyPartyApp;
using Infrastructure.ConstantsDefine.HardCode;
using Infrastructure.ConstantsDefine.KeywordTranslate;
using Models.EntityModels;
using Models.Models.Request;
using Models.Models.Response;
using Models.Models.Result;
using Repository.Implementation;
using Repository.Interfaces;

namespace Business.APIBusinessServices.DistrictService
{
	public class DistrictCreateService : BaseBusinessServices<DistrictCreateRequest, DistrictCreateResponse>
    {
        private readonly IDistrictRepository _districtRepository;
        //private readonly ICityRepository _cityRepository;
        private District? newDistrict;
        private Guid newSiteID = Guid.NewGuid();


        public DistrictCreateService(
            IDistrictRepository districtRepository,
            //ICityRepository cityRepository,
            SlackSendMessageServices slackSendMessageServices,
            IHttpContextAccessor httpContextAccessor,
            string successMessageDefault = ""            
        ) : base(slackSendMessageServices, httpContextAccessor, successMessageDefault)
        {
            _districtRepository = districtRepository;
            //_cityRepository = cityRepository;
        }

        public override void P1GenerateObjects()
        {
            newDistrict = new District
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
                CityId = _dataRequest.CityId
            };
        }

        public override void P2PostValidation()
        {
            //if (_dataRequest.CityId == null)
            //{
            //    throw new BaseExceptionResult { Messages = SiteKeywords.COUNTRY_NOT_EXIST };
            //}

            //City? city = _cityRepository.GetBy(s => s.Code == newDistrict.CityId).FirstOrDefault();
            //if (city == null)
            //{
            //    throw new BaseExceptionResult { Messages = SiteKeywords.CITY_NOT_EXIST };
            //}

        }

        public override void P3AccessDatabase()
        {

            _districtRepository.Add(newDistrict);
            _districtRepository.Save();
        }

        public override void P4GenerateResponseData()
        {
            _dataResponse = new DistrictCreateResponse
            {
                Id = newSiteID
            };
        }
    }
}

