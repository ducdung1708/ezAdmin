using System;
using Business.APIBusinessServices.ThirtyPartyApp;
using Infrastructure.ConstantsDefine.KeywordTranslate;
using Models.Models.Request;
using Models.Models.Response;
using Models.Models.Result;
using Repository.Interfaces;

namespace Business.APIBusinessServices.CityService
{
    public class CityDeleteService : BaseBusinessServices<Guid?, CityDeleteResponse>
    {
        private readonly ICityRepository _cityRepository;
        private int nr;

        public CityDeleteService(
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
            
        }

        public override void P2PostValidation()
        {
            if (_dataRequest == null || !_dataRequest.HasValue)
            {
                throw new BaseExceptionResult { Messages = ValidationKeywords.VALIDATION_FAIL };
            }
            
        }

        public override void P3AccessDatabase()
        {
             _cityRepository.Delete<Guid>(_dataRequest.Value);
             nr = _cityRepository.Save();
        }

        public override void P4GenerateResponseData()
        {
            _dataResponse = new CityDeleteResponse
            {
                Id = _dataRequest.Value,
                NumberOfRows = nr
            };
        }
    }
}
