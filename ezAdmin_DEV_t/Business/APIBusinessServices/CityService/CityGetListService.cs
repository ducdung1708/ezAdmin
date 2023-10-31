using System;
using System.Linq.Expressions;
using Business.APIBusinessServices.ThirtyPartyApp;
using Models.EntityModels;
using Models.Models.Request;
using Models.Models.Response;
using Repository.Interfaces;
namespace Business.APIBusinessServices.CityService
{
	public class CityGetListService:BaseBusinessServices<CityGetListRequest, List<CityGetListResponse>>
	{
        private readonly ICityRepository _cityRepository;
        private List<CityGetListResponse> _listCityQuery;

        public CityGetListService(
            ICityRepository cityRepository,
            SlackSendMessageServices slackSendMessageServices,
            IHttpContextAccessor httpContextAccessor,
            string successMessageDefault = ""
            ) : base(slackSendMessageServices, httpContextAccessor, successMessageDefault)
        {
            _cityRepository = cityRepository;
        }

        //Preprocess data request before using
        //Make data is beautiful
        public override void P1GenerateObjects()
        {
            _dataRequest.Name = _dataRequest.Name ?? "";
            _dataRequest.PageIndex = _dataRequest.PageIndex ?? 1;
            _dataRequest.PageSize = _dataRequest.PageSize ?? 20;
        }

        /// <summary>
        /// Check data valid???
        /// </summary>
        public override void P2PostValidation()
        {

        }

        public override void P3AccessDatabase()
        {
            int skipRows = _dataRequest.PageSize.Value * (_dataRequest.PageIndex.Value - 1);

            Expression<Func<City, bool>> cityConditionSearch = s => (s.Name ?? "").Contains(_dataRequest.Name);

            _listCityQuery = _cityRepository
                .GetBy(cityConditionSearch)
                .Select(s => new CityGetListResponse
                {
                    Id = s.Id,
                    Name = s.Name,
                    Code = s.Code

                })
                .Skip(skipRows)
                .Take(_dataRequest.PageSize.Value)
                .ToList();
        }

        public override void P4GenerateResponseData()
        {
            _dataResponse = _listCityQuery;
        }
    }
}

