using Business.APIBusinessServices.ThirtyPartyApp;
using Infrastructure.ConstantsDefine;
using Infrastructure.ConstantsDefine.HardCode;
using Infrastructure.ConstantsDefine.KeywordTranslate;
using Infrastructure.Core.Cache;
using Models.Models.Result;
using Repository.Interfaces;

namespace Business.APIBusinessServices.Language
{
    public class GetLanguageService :BaseBusinessServices<string, Dictionary<string,string>>
    {
        private readonly ICacheService _cacheService;
        private readonly ILanguageRepository _languageRepository;

        public GetLanguageService(
            ICacheService cacheService, 
            ILanguageRepository languageRepository,
            SlackSendMessageServices slackSendMessageServices,
            IHttpContextAccessor httpContextAccessor, 
            string successMessageDefault = CommonKeywords.GET_DATA_SUCESS) 
            : base(slackSendMessageServices, httpContextAccessor, successMessageDefault)
        {
            _cacheService = cacheService;
            _languageRepository = languageRepository;
        }

        private string HashKeyLanguage
        {
            get
            {
                return _cacheService.GetHashKey(RedisCacheKeys.LOCALE_LANGUAGE, _dataRequest);
            }
        }

        public override void P1GenerateObjects()
        {
            _dataRequest = (_dataRequest ?? "").Trim();
            if (string.IsNullOrEmpty(_dataRequest) || string.IsNullOrWhiteSpace(_dataRequest))
            {
                throw new BaseExceptionResult { Messages = ValidationKeywords.LANGAUAGE_CODE_IS_REQUIRED };
            }
            if (!LocaleLanguageCodes.LanguageCodes.Contains(_dataRequest))
            {
                throw new BaseExceptionResult { Messages = ValidationKeywords.LANGAUAGE_CODE_INCORRECT };
            }
        }

        public override void P2PostValidation()
        {
        }

        public override void P3AccessDatabase()
        {
            bool existLanguageCache = _cacheService.Any(RedisCacheKeys.LOCALE_LANGUAGE, HashKeyLanguage);
            if (existLanguageCache)
            {
                _dataResponse = _cacheService.Get<Dictionary<string, string>>(RedisCacheKeys.LOCALE_LANGUAGE, HashKeyLanguage);
            }
            else
            {
                _dataResponse = _languageRepository.GetLanguage(_dataRequest);
                _cacheService.AddOrUpdate(RedisCacheKeys.LOCALE_LANGUAGE, HashKeyLanguage, _dataResponse);
            }
        }

        public override void P4GenerateResponseData()
        {
        }
    }
}
