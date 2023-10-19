using System.Reflection;

namespace Infrastructure.ConstantsDefine.HardCode
{
    public class SiteStatus
    {
        public const string ACTIVE = "ACTIVE";
        public const string DEACTIVE = "DEACTIVE";

        public static List<string> SiteStatusList
        {
            get
            {
                return typeof(SiteStatus).GetFields(BindingFlags.Static | BindingFlags.Public)
                    .Select(x => x.GetValue(null)
                    .ToString())
                    .ToList();
            }
        }
    }
}
