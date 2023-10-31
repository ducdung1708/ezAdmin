using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.APIBusinessServices.CityService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models.Request;
using Models.Models.Response;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers.CI00_Cities
{

    [Route("api/ci00-city")]
    public class CityController : ControllerBase
    {
        private readonly CityGetListService _cityGetListService;
        private readonly CityGetDetailService _cityGetDetailService;
        private readonly CityCreateService _cityCreateService;
        private readonly CityUpdateService _cityUpdateService;
        private readonly CityDeleteService _cityDeleteService;

        public CityController(
            CityGetListService cityGetListService,
            CityGetDetailService cityGetDetailService,
            CityCreateService cityCreateService,
            CityUpdateService cityUpdateService,
            CityDeleteService cityDeleteService
            )
        {
            _cityGetListService = cityGetListService;
            _cityCreateService = cityCreateService;
            _cityGetDetailService = cityGetDetailService;
            _cityUpdateService = cityUpdateService;
            _cityDeleteService = cityDeleteService;
        }

        /// <summary>
        /// Get list of cities based on a filter request
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("ci01-get-list")]
        public IActionResult GetList(CityGetListRequest cityGetListRequest)
        {
            BaseResponse<List<CityGetListResponse>> result = _cityGetListService.Process(cityGetListRequest);

            return Ok(result);
        }

        /// <summary>
        /// Get a detail info of a city based on an id
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("ci02-get-detail")]
        public IActionResult GetDetail(Guid? cityId)
        {
            BaseResponse<CityGetDetailResponse> result = _cityGetDetailService.Process(cityId);
            return Ok(result);
        }

        /// <summary>
        /// Create a new city with a detail info
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("ci03-create-city")]
        public IActionResult CreateCity(CityCreateRequest cityCreateRequest)
        {
            BaseResponse<CityCreateResponse> result = _cityCreateService.Process(cityCreateRequest);
            return Ok(result);
        }

        /// <summary>
        /// Update a new city with a detail info and id
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("ci04-update-city")]
        public IActionResult UpdateCity(CityUpdateRequest cityUpdateRequest)
        {
            BaseResponse<CityUpdateResponse> result = _cityUpdateService.Process(cityUpdateRequest);
            return Ok(result);
        }

        ///// <summary>
        ///// Update a new city with a detail info and id
        ///// </summary>
        ///// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("ci05-delete-city")]
        public IActionResult DeleteCity(Guid? cityId)
        {
            BaseResponse<CityDeleteResponse> result = _cityDeleteService.Process(cityId);
            return Ok(result);
        }
    }
}
