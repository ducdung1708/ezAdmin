using System.Reflection;

namespace Infrastructure.ConstantsDefine.HardCode
{
    public class LocaleLanguageCodes
    {
        /// <summary>
        /// Tiếng Việt
        /// </summary>
        public const string VI = "vi";

        /// <summary>
        /// Tiếng Anh
        /// </summary>
        public const string EN = "en";

        public static List<string> LanguageCodes
        {
            get
            {
                Type languageCodes = typeof(LocaleLanguageCodes);
                return languageCodes.GetFields(BindingFlags.Static | BindingFlags.Public)
                    .Select(x => x.GetValue(null)
                    .ToString())
                    .ToList();
            }
        }
    }
}
