using System;
using System.Collections.Generic;
using Business.APIBusinessServices.CountryServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models.Request;
using Models.Models.Response;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers.CT00_Country
{
    [Route("api/ct00-country")]
    public class CountryController : ControllerBase
    {
        private readonly CountryGetListService _countryGetListService;
        private readonly CountryGetDetailService _countryGetDetailService;
        private readonly CountryCreateService _countryCreateService;
        private readonly CountryUpdateService _countryUpdateService;
        private readonly CountryDeleteService _countryDeleteService;

        public CountryController(
            CountryGetListService countryGetListService,
            CountryGetDetailService countryGetDetailService,
            CountryCreateService countryCreateService,
            CountryUpdateService countryUpdateService,
            CountryDeleteService countryDeleteService
            )
        {
            _countryGetListService = countryGetListService;
            _countryGetDetailService = countryGetDetailService;
            _countryCreateService = countryCreateService;
            _countryUpdateService = countryUpdateService;
            _countryDeleteService = countryDeleteService;
        }

        /// <summary>
        /// Get list of companies based on a filter request
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("ct01-get-list")]
        public IActionResult GetList(CountryGetListRequest countryGetListRequest )
        {
            BaseResponse<List<CountryGetListResponse>> result = _countryGetListService.Process(countryGetListRequest);

            return Ok(result);
        }

        /// <summary>
        /// Get a detail info of a country based on an id
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("ct02-get-detail")]
        public IActionResult GetDetail(int countryId)
        {
            BaseResponse<CountryGetDetailResponse> result = _countryGetDetailService.Process(countryId);
            return Ok(result);
        }

        /// <summary>
        /// Create a new country with a detail info
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("ct03-create-country")]
        public IActionResult CreateCountry([FromBody] CountryCreateRequest countryCreateRequest)
        {
            BaseResponse<CountryCreateResponse> result = _countryCreateService.Process(countryCreateRequest);
            return Ok(result);
        }

        /// <summary>
        /// Update a new country with a detail info and id
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("ct04-update-country")]
        public IActionResult UpdateCountry([FromBody] CountryUpdateRequest countryUpdateRequest)
        {
            BaseResponse<CountryUpdateResponse> result = _countryUpdateService.Process(countryUpdateRequest);
            return Ok(result);
        }

        /// <summary>
        /// Update a new country with a detail info and id
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("ct04-delete-country")]
        public IActionResult DeleteCountry([FromBody] int countryId)
        {
            BaseResponse<CountryDeleteResponse> result = _countryDeleteService.Process(countryId);
            return Ok(result);
        }

    }
}

