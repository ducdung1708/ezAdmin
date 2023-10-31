using Business.APIBusinessServices.Account;
using Models.Models.Request;
using Models.Models.Response;
using Models.ModelValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Business.APIBusinessServices.AccountServices;

namespace Api.Controllers.AU00_Account
{
    /// <summary>
    /// "AU00"
    /// </summary>
    [Route("api/au00-account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountAuthorizedServices _accountAuthorizedServices;
        private readonly AccountSignOutServices _accountSignOutServices;        
        private readonly UserSiteInfoGetSessionServices _userSiteInfoGetSessionServices;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authorizedAccountServices"></param>
        /// <param name="AccountSignOutServices"></param>
        /// <param name="SiteSwitchServices"></param>
        /// <param name="UserSiteInfoGetSessionServices"></param>
        public AccountController(
            AccountAuthorizedServices authorizedAccountServices,
            AccountSignOutServices accountSignOutServices,            
            UserSiteInfoGetSessionServices userSiteInfoGetSessionServices) 
        {
            _accountAuthorizedServices = authorizedAccountServices;
            _accountSignOutServices = accountSignOutServices;            
            _userSiteInfoGetSessionServices = userSiteInfoGetSessionServices;
        }

        /// <summary>
        /// Xác thực User login ezID
        /// </summary>
        /// <param name="accountRequestModels"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ValidateModel]
        [Route("au01-verify-tokenezid")]
        public IActionResult ezIDAuthorized(AccountVerifyRequest accountRequestModels)
        {
            BaseResponse<AccountVerifyResponse> accountResponseResult = _accountAuthorizedServices.Process(accountRequestModels);
            return Ok(accountResponseResult);
        }

        /// <summary>
        /// Đăng xuất
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("au02-signout")]
        public IActionResult Signout()
        {
            BaseResponse<object> signoutAccountResponseResult = _accountSignOutServices.Process();
            return Ok(signoutAccountResponseResult);
        }

        
        /// <summary>
        /// Lấy thông tin Session User đang truy cập
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [ValidateModel]
        [Route("au04-get-sessioninfo")]
        public IActionResult GetSessionUserSiteInfo()
        {
            BaseResponse<SessionUserSiteInfoResponse> result = _userSiteInfoGetSessionServices.Process();
            return Ok(result);
        }
    }
}
