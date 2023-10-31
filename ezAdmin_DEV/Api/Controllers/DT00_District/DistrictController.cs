using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.APIBusinessServices.DistrictService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models.Request;
using Models.Models.Response;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers.DT00_District
{
    [Route("api/dt00-district")]
    public class DistrictController : ControllerBase
    {
        private readonly DistrictGetListService _districtGetListService;
        private readonly DistrictGetDetailService _districtGetDetailService;
        private readonly DistrictCreateService _districtCreateService;
        private readonly DistrictUpdateService _districtUpdateService;
        private readonly DistrictDeleteService _districtDeleteService;

        public DistrictController(
            DistrictGetListService districtGetListService,
            DistrictGetDetailService districtGetDetailService,
            DistrictCreateService districtCreateService,
            DistrictUpdateService districtUpdateService,
            DistrictDeleteService districtDeleteService
            )
        {
            _districtGetListService = districtGetListService;
            _districtCreateService = districtCreateService;
            _districtGetDetailService = districtGetDetailService;
            _districtUpdateService = districtUpdateService;
            _districtDeleteService = districtDeleteService;
        }

        /// <summary>
        /// Get list of district based on a filter request
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("dt01-get-list")]
        public IActionResult GetList(DistrictGetListRequest districtGetListRequest)
        {
            BaseResponse<List<DistrictGetListResponse>> result = _districtGetListService.Process(districtGetListRequest);

            return Ok(result);
        }

        /// <summary>
        /// Get a detail info of a district based on an id
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("dt02-get-detail")]
        public IActionResult GetDetail(Guid? districtId)
        {
            BaseResponse<DistrictGetDetailResponse> result = _districtGetDetailService.Process(districtId);
            return Ok(result);
        }

        /// <summary>
        /// Create a new district with a detail info
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("dt03-create-district")]
        public IActionResult CreateDistrict([FromBody] DistrictCreateRequest districtCreateRequest)
        {
            BaseResponse<DistrictCreateResponse> result = _districtCreateService.Process(districtCreateRequest);
            return Ok(result);
        }

        /// <summary>
        /// Update a new district with a detail info and id
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("dt04-update-district")]
        public IActionResult UpdateDistrict([FromBody] DistrictUpdateRequest districtUpdateRequest)
        {
            BaseResponse<DistrictUpdateResponse> result = _districtUpdateService.Process(districtUpdateRequest);
            return Ok(result);
        }

        ///// <summary>
        ///// Update a new district with a detail info and id
        ///// </summary>
        ///// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("dt05-delete-district")]
        public IActionResult DeleteDistrict([FromBody] Guid? districtId)
        {
            BaseResponse<DistrictDeleteResponse> result = _districtDeleteService.Process(districtId);
            return Ok(result);
        }
    }
}

