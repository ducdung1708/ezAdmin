using System.Reflection;

namespace Infrastructure.ConstantsDefine.HardCode
{
    public class InvoiceUpdateType
    {
        public const string ADJUST_VALUE = "AdjustValue";
        public const string ADJUST_DECREASE = "AdjustDecrease";
        public const string ADJUST_INCREASE = "AdjustIncrease";
        public const string ADJUST_INFOR = "AdjustInfor";

        public static List<string> InvoiceUpdateTypes
        {
            get
            {
                return typeof(InvoiceUpdateType).GetFields(BindingFlags.Static | BindingFlags.Public)
                    .Select(x => x.GetValue(null)
                    .ToString())
                    .ToList();
            }
        }
    }
}
