using System;
using System.Collections.Generic;
using Business.APIBusinessServices.CompanyService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models.Request;
using Models.Models.Response;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers.CP00_Company
{
    [Route("api/cp00-company")]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyGetListService _companyGetListService;
        private readonly CompanyGetDetailService _companyGetDetailService;
        private readonly CompanyCreateService _companyCreateService;
        private readonly CompanyUpdateService _companyUpdateService;
        private readonly CompanyDeleteService _companyDeleteService;

        public CompanyController(
            CompanyGetListService companyGetListService,
            CompanyGetDetailService companyGetDetailService,
            CompanyCreateService companyCreateService,
            CompanyUpdateService companyUpdateService,
            CompanyDeleteService companyDeleteService
            )
        {
            _companyGetListService = companyGetListService;
            _companyGetDetailService = companyGetDetailService;
            _companyCreateService = companyCreateService;
            _companyUpdateService = companyUpdateService;
            _companyDeleteService = companyDeleteService;
        }

        /// <summary>
        /// Get list of companies based on a filter request
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("cp01-get-list")]
        public IActionResult GetList(CompanyGetListRequest companyGetListRequest )
        {
            BaseResponse<List<CompanyGetListResponse>> result = _companyGetListService.Process(companyGetListRequest);

            return Ok(result);
        }

        /// <summary>
        /// Get a detail info of a company based on an id
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("cp02-get-detail")]
        public IActionResult GetDetail(Guid? companyId)
        {
            BaseResponse<CompanyGetDetailResponse> result = _companyGetDetailService.Process(companyId);
            return Ok(result);
        }

        /// <summary>
        /// Create a new company with a detail info
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("cp03-create-company")]
        public IActionResult CreateCompany(CompanyCreateRequest companyCreateRequest)
        {
            BaseResponse<CompanyCreateResponse> result = _companyCreateService.Process(companyCreateRequest);
            return Ok(result);
        }

        /// <summary>
        /// Update a new company with a detail info and id
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("cp04-update-company")]
        public IActionResult UpdateCompany(CompanyUpdateRequest companyUpdateRequest)
        {
            BaseResponse<CompanyUpdateResponse> result = _companyUpdateService.Process(companyUpdateRequest);
            return Ok(result);
        }

        /// <summary>
        /// Update a new company with a detail info and id
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("cp05-delete-company")]
        public IActionResult DeleteCompany(Guid? companyId)
        {
            BaseResponse<CompanyDeleteResponse> result = _companyDeleteService.Process(companyId);
            return Ok(result);
        }

    }
}

