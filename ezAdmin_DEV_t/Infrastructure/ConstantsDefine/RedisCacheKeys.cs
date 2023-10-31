namespace Infrastructure.ConstantsDefine
{
    public class RedisCacheKeys
    {
        // Key
        public const string USER_SESSION = "ezINV_UserSessions";
        public const string LOCALE_LANGUAGE = "ezINV_LocaleLanguage";
        public const string RESOURCES = "ezINV_Resources";
        public const string PDF = "ezINV_PDF";
        // CacheName (Hash Key)
        public const string RESOURCE_COUNTRIES = "Countries";
        public const string RESOURCE_PARTNERS = "Partners";
        public const string RESOURCE_CLIENT_CONNECTS = "ClientConnects";
        public const string RESOURCE_PAYMENT_METHODS = "PaymentMethods";
    }
}
