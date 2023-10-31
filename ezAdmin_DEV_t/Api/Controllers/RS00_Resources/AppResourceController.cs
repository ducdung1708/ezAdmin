using Business.APIBusinessServices.Resource;
using Models.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.RS00_Resources
{
    /// <summary>
    /// "RS00"
    /// </summary>
    [Route("api/rs00-app")]
    [ApiController]
    public class AppResourceController : ControllerBase
    {
        private readonly GetResourceAppServices _getResourceAppServices;
        public AppResourceController(GetResourceAppServices getResourceAppServices)
        {
            _getResourceAppServices = getResourceAppServices;
        }

        /// <summary>
        /// Danh sách Resource khi login hệ thống
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("rs02-get-appresources")]
        public IActionResult GetAppResource()
        {
            BaseResponse<ResourceAppResponse> resultData = _getResourceAppServices.Process();
            return Ok(resultData);
        }
    }
}
