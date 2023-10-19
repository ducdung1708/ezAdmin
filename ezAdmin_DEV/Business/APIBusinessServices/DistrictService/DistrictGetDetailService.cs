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

namespace Business.APIBusinessServices.DistrictService
{
	public class DistrictGetDetailService : BaseBusinessServices<Guid?, DistrictGetDetailResponse>
    {
        private readonly IDistrictRepository _districtRepository;       
        private DistrictDetailResult? _districtDetailResult;

        public DistrictGetDetailService(
            IDistrictRepository districtRepository,
            SlackSendMessageServices slackSendMessageServices,
            IHttpContextAccessor httpContextAccessor,            
            string successMessageDefault = ""
        ) : base(slackSendMessageServices, httpContextAccessor, successMessageDefault)
        {
            _districtRepository = districtRepository;
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

            _districtDetailResult = _districtRepository.GetDistrictDetail(_dataRequest.Value);

            if (_districtDetailResult == null)
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
            
            _dataResponse = new DistrictGetDetailResponse
            {
                Id = _districtDetailResult.Id,
                Name = _districtDetailResult.Name,
                Code = _districtDetailResult.Code,
                AirportCode = _districtDetailResult.AirportCode,
                PhoneCode = _districtDetailResult.PhoneCode,
                ExtraCode = _districtDetailResult.ExtraCode,
                PostalCode = _districtDetailResult.PostalCode,
                Latitude = _districtDetailResult.Latitude,
                Longtitude = _districtDetailResult.Longtitude,
                SortIndex = _districtDetailResult.SortIndex,
                Active = _districtDetailResult.Active,
                CreatedDate = _districtDetailResult.CreatedDate,
                CreatedBy = _districtDetailResult.CreatedBy,
                UpdatedDate = _districtDetailResult.UpdatedDate,
                UpdatedBy = _districtDetailResult.UpdatedBy,
                CityId = _districtDetailResult.CityId,
            };
        }
    }
}

