using System;
using System.Linq.Expressions;
using Business.APIBusinessServices.ThirtyPartyApp;
using Models.EntityModels;
using Models.Models.Request;
using Models.Models.Response;
using Repository.Interfaces;
namespace Business.APIBusinessServices.DistrictService
{
	public class DistrictGetListService:BaseBusinessServices<DistrictGetListRequest, List<DistrictGetListResponse>>
	{
        private readonly IDistrictRepository _districtRepository;
        private List<DistrictGetListResponse> _listDistrictQuery;

        public DistrictGetListService(
            IDistrictRepository districtRepository,
            SlackSendMessageServices slackSendMessageServices,
            IHttpContextAccessor httpContextAccessor,
            string successMessageDefault = ""
            ) : base(slackSendMessageServices, httpContextAccessor, successMessageDefault)
        {
            _districtRepository = districtRepository;
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

            Expression<Func<District, bool>> districtConditionSearch = s => (s.Name ?? "").Contains(_dataRequest.Name);

            _listDistrictQuery = _districtRepository
                .GetBy(districtConditionSearch)
                .Select(s => new DistrictGetListResponse
                {
                    Id = s.Id,
                    Name = s.Name,
                    Code = s.Code,
                    CityId = s.CityId

                })
                .Skip(skipRows)
                .Take(_dataRequest.PageSize.Value)
                .ToList();
        }

        public override void P4GenerateResponseData()
        {
            _dataResponse = _listDistrictQuery;
        }
    }
}

