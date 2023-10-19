using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Numerics;
using Business.APIBusinessServices.ThirtyPartyApp;
using Infrastructure.ConstantsDefine.KeywordTranslate;
using Models.Models.Response;
using Models.Models.Result;
using Repository.Implementation;
using Repository.Interfaces;

namespace Business.APIBusinessServices.CityService
{
	public class CityGetDetailService : BaseBusinessServices<Guid?, CityGetDetailResponse>
    {
        private readonly ICityRepository _cityRepository;       
        private CityDetailResult? _cityDetailResult;

        public CityGetDetailService(
            ICityRepository cityRepository,
            SlackSendMessageServices slackSendMessageServices,
            IHttpContextAccessor httpContextAccessor,            
            string successMessageDefault = ""
        ) : base(slackSendMessageServices, httpContextAccessor, successMessageDefault)
        {
            _cityRepository = cityRepository;
        }

        public override void P1GenerateObjects()
        {
            if (!_dataRequest.HasValue)
            {
                throw new BaseExceptionResult { Messages = ValidationKeywords.VALIDATION_FAIL };
            }         
        }

        public override void P2PostValidation()
        {
            
        }

        public override void P3AccessDatabase()
        {

            _cityDetailResult = _cityRepository.GetCityDetail(_dataRequest.Value);

            if (_cityDetailResult == null)
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
            
            _dataResponse = new CityGetDetailResponse
            {
                Id = _cityDetailResult.Id,
                Name = _cityDetailResult.Name,
                Code = _cityDetailResult.Code,
                AirportCode = _cityDetailResult.AirportCode,
                PhoneCode = _cityDetailResult.PhoneCode,
                ExtraCode = _cityDetailResult.ExtraCode,
                PostalCode = _cityDetailResult.PostalCode,
                Latitude = _cityDetailResult.Latitude,
                Longtitude = _cityDetailResult.Longtitude,
                SortIndex = _cityDetailResult.SortIndex,
                Active = _cityDetailResult.Active,
                CreatedDate = _cityDetailResult.CreatedDate,
                CreatedBy = _cityDetailResult.CreatedBy,
                UpdatedDate = _cityDetailResult.UpdatedDate,
                UpdatedBy = _cityDetailResult.UpdatedBy,
                CountryId = _cityDetailResult.CountryId,
            };
        }
    }
}

