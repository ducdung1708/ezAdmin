using System.Reflection;

namespace Infrastructure.ConstantsDefine.HardCode
{
    public class InvoiceCreateStatus
    {
        public const string DRAFT = "Draft";
        public const string OFFICIAL = "Official";

        public static List<string> ListInvoiceCreateStatus
        {
            get
            {
                return typeof(InvoiceCreateStatus).GetFields(BindingFlags.Static | BindingFlags.Public)
                    .Select(x => x.GetValue(null)
                    .ToString())
                    .ToList();
            }
        }
    }
}
