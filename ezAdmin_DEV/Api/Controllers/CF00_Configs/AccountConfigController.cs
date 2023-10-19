using Business.APIBusinessServices.Account;
using Models.Models.Request;
using Models.Models.Response;
using Models.ModelValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Api.Controllers.CF00_Configs
{
    /// <summary>
    /// "CF01"
    /// </summary>
    [Route("api/cf01-account")]
    [ApiController]
    public class AccountConfigController : ControllerBase
    {

        private readonly AccountGetListServices _accountGetListServices;
        private readonly AccountUpdateStatusServices _accountUpdateStatusServices;
        private readonly AccountDeleteServices _accountDeleteServices;
        private readonly AccountUpdateService   _accountUpdateService;
        private readonly AccountInviteServices _accountInviteServices;
        private readonly UserRoleChangeGroupServices _userRoleChangeGroupServices;

        public AccountConfigController
            (AccountGetListServices accountGetListServices,
            AccountUpdateStatusServices accountUpdateStatusServices,
            AccountDeleteServices accountDeleteServices,
            AccountUpdateService accountUpdateService,
            AccountInviteServices accountInviteServices,
            UserRoleChangeGroupServices userRoleChangeGroupServices)
        {
            _accountGetListServices = accountGetListServices;
            _accountDeleteServices = accountDeleteServices;
            _accountUpdateService = accountUpdateService;
            _accountUpdateStatusServices = accountUpdateStatusServices;
            _accountInviteServices = accountInviteServices;
            _userRoleChangeGroupServices = userRoleChangeGroupServices;
        }

        /// <summary>
        /// Danh sách tài khoản theo Site
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("cf011-get-list")]
        public IActionResult GetBySite(Guid? siteId = null)
        {
            BaseResponse<List<AccountGetListResponse>> UserResult = _accountGetListServices.Process(siteId);
            return Ok(UserResult);
        }

        /// <summary>
        /// Mời tài khoản tham gia vào Site
        /// </summary>
        /// <param name="accountInviteRequest"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [ValidateModel]
        [Route("cf012-invite-account")]
        public IActionResult InviteAccount(AccountInviteRequest accountInviteRequest)
        {
            BaseResponse<object> result = _accountInviteServices.Process(accountInviteRequest);
            return Ok(result);
        }

        /// <summary>
        /// Chuyển nhóm Quyền
        /// </summary>
        /// <param name="accountChangeRoleGroupRequest"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [ValidateModel]
        [Route("cf014-change-groupuserrole")]
        public IActionResult ChangeGroupUserRole(AccountChangeRoleGroupRequest accountChangeRoleGroupRequest)
        {
            BaseResponse<object> result = _userRoleChangeGroupServices.Process(accountChangeRoleGroupRequest);
            return Ok(result);
        }

        /// <summary>
        /// Cập nhật trạng thái tài khoản
        /// </summary>
        /// <param name="AccountUpdateStatusServices"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("cf015-update-usersitestatus")]
        public IActionResult UpdateStatusAccount(AccountUpdateStatusRequest accountUpdateStatusServices)
        {
            BaseResponse<object> result = _accountUpdateStatusServices.Process(accountUpdateStatusServices);
            return Ok(result);
        }
        /// <summary>
        /// Xóa tài khoản
        /// </summary>
        /// <param name="accountDeleteRequest"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("cf016-delete")]
        public IActionResult DeleteAccount(AccountDeleteRequest accountDeleteRequest)
        {
            BaseResponse<object> result = _accountDeleteServices.Process(accountDeleteRequest);
            return Ok(result);
        }
        /// <summary>
        /// Cập nhật tài khoản
        /// </summary>
        /// <param name="accountUpdateRequest"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("cf013-update")]

        public IActionResult UpdateAccount (AccountUpdateRequest accountUpdateRequest)
        {
            BaseResponse<object> result = _accountUpdateService.Process(accountUpdateRequest);
            return Ok(result);
        }
    }
}