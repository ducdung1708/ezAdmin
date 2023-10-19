using Business.APIBusinessServices.Language;
using Models.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Api.Controllers.RS00_Resources
{
    /// <summary>
    /// "RS00" Ngôn ngữ dịch cho Client
    /// </summary>
    [Route("api/rs00-language")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly GetLanguageService _getLanguageService;
        public LanguageController(GetLanguageService getLanguageService)
        {
            _getLanguageService = getLanguageService;
        }

        /// <summary>
        /// Get language based on language code
        /// </summary>
        /// <param name="languageCode">Mã ngôn ngữ</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("rs01-get-translations")]
        public IActionResult GetLanguageByCode(string languageCode)
        {
            BaseResponse<Dictionary<string,string>> LanguageResult = _getLanguageService.Process(languageCode);
            return Ok(LanguageResult);
        }
    }
}
