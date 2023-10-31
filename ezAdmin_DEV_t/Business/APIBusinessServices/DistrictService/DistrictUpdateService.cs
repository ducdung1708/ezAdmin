using Business.APIBusinessServices.ThirtyPartyApp;
using Infrastructure.ConstantsDefine.KeywordTranslate;
using Models.EntityModels;
using Models.Models.Request;
using Models.Models.Response;
using Models.Models.Result;
using Repository.Interfaces;

namespace Business.APIBusinessServices.DistrictService
{
    public class DistrictUpdateService : BaseBusinessServices<DistrictUpdateRequest, DistrictUpdateResponse>
    {
        private readonly IDistrictRepository _districtRepository;
        private readonly ICityRepository _cityRepository;
        private District updatedDistrict;
        private Guid newSiteID = Guid.NewGuid();
        private bool checkCityUpdate = false;
        private int nr = 0;

        public DistrictUpdateService (
            IDistrictRepository districtRepository,
            ICityRepository cityRepository,
            SlackSendMessageServices slackSendMessageServices,
            IHttpContextAccessor httpContextAccessor,
            string successMessageDefault = ""
        ) : base(slackSendMessageServices, httpContextAccessor, successMessageDefault)
        {
            _districtRepository = districtRepository;
            _cityRepository = cityRepository;
        }

        public override void P1GenerateObjects()
        {
            updatedDistrict = _districtRepository.GetById<Guid?>(_dataRequest.Id);
            if (updatedDistrict == null)
            {
                throw new BaseExceptionResult { Messages = SiteKeywords.SITE_NOT_EXIST };
            }

            checkCityUpdate = !string.IsNullOrEmpty(_dataRequest.CityId.ToString()) && _dataRequest.CityId != updatedDistrict.CityId;

            updatedDistrict.Name = _dataRequest.Name ?? updatedDistrict.Name;
            updatedDistrict.Code = _dataRequest.Code ?? updatedDistrict.Code;
            updatedDistrict.AirportCode = _dataRequest.AirportCode ?? updatedDistrict.AirportCode;
            updatedDistrict.PhoneCode = _dataRequest.PhoneCode ?? updatedDistrict.PhoneCode;
            updatedDistrict.ExtraCode = _dataRequest.ExtraCode ?? updatedDistrict.ExtraCode;
            updatedDistrict.PostalCode = _dataRequest.PostalCode ?? updatedDistrict.PostalCode;
            updatedDistrict.Latitude = _dataRequest.Latitude ?? updatedDistrict.Latitude;
            updatedDistrict.Longtitude = _dataRequest.Longtitude ?? updatedDistrict.Longtitude;
            updatedDistrict.SortIndex = _dataRequest.SortIndex ?? updatedDistrict.SortIndex;
            updatedDistrict.Active = _dataRequest.Active ?? updatedDistrict.Active;
            updatedDistrict.CreatedDate = _dataRequest.CreatedDate ?? updatedDistrict.CreatedDate;
            updatedDistrict.CreatedBy = _dataRequest.CreatedBy ?? updatedDistrict.CreatedBy;
            updatedDistrict.UpdatedDate = _dataRequest.UpdatedDate ?? updatedDistrict.UpdatedDate;
            updatedDistrict.UpdatedBy = _dataRequest.UpdatedBy ?? updatedDistrict.UpdatedBy;
            updatedDistrict.CityId = _dataRequest.CityId ?? updatedDistrict.CityId;
        }

        public override void P2PostValidation()
        {
            if (checkCityUpdate)
            {
                City? city = _cityRepository
                    .GetBy(s => s.Id == updatedDistrict.Id)
                    .FirstOrDefault();

                if (city == null)
                {
                    throw new BaseExceptionResult { Messages = SiteKeywords.CITY_NOT_EXIST};
                }
            }

        }

        public override void P3AccessDatabase()
        {
            if (updatedDistrict != null)
            {
                //build query
                _districtRepository.Update(updatedDistrict);

                //execute query
                nr = _districtRepository.Save();
            }
        }

        public override void P4GenerateResponseData()
        {
            _dataResponse = new DistrictUpdateResponse
            {
                Id = newSiteID,
                NumberOfRows = nr
            };
        }
    }
}

