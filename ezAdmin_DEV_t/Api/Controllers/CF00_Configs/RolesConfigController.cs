using Business.APIBusinessServices.Role;
using Models.Models.Request;
using Models.Models.Response;
using Models.ModelValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Business.APIBusinessServices.Menu;

namespace Api.Controllers.CF00_Configs
{
    /// <summary>
    /// "CF02"
    /// </summary>
    [Route("api/cf02-roles")]
    [ApiController]
    public class RolesConfigController : ControllerBase
    {
        private readonly RoleGetServices _roleGetServices;
        private readonly RoleUpdateServices _roleUpdateServices;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="getMenuServices"></param>
        /// <param name="RoleUpdateServices"></param>
        public RolesConfigController(
            RoleGetServices getMenuServices, 
            RoleUpdateServices roleUpdateServices)
        {
            _roleGetServices = getMenuServices;
            _roleUpdateServices = roleUpdateServices;
        }

        /// <summary>
        /// Lấy Danh Sách Role theo Nhóm người dùng
        /// </summary>
        /// <param name="groupUserID"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("cf021-get-listrolesgroup")]
        public IActionResult GetBySite(Guid? groupUserID = null)
        {
            BaseResponse<List<RolesGroupUserResponse>> getRoleResult = _roleGetServices.Process(groupUserID);
            return Ok(getRoleResult);
        }

        /// <summary>
        /// Cập nhật danh sach role của nhóm người dùng
        /// </summary>
        /// <param name="groupUserRoleUpdateRequest"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [ValidateModel]
        [Route("cf022-update-listrolesgroup")]
        public IActionResult UpdateGroupUserRole(GroupUserRoleUpdateRequest groupUserRoleUpdateRequest)
        {
            BaseResponse<object> updateRoleResult = _roleUpdateServices.Process(groupUserRoleUpdateRequest);
            return Ok(updateRoleResult);
        }
    }
}
