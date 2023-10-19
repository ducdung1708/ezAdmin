using Infrastructure.Enums;

namespace Infrastructure.ConstantsDefine
{
    public class SyntheticData
    {
        public static List<int> InvoiceTypes
        {
            get
            {
                return ((InvoiceType[])Enum.GetValues(typeof(InvoiceType)))
                     .Select(c => (int)c)
                     .ToList();
            }
        }

        public static List<int> EInvoiceTypes
        {
            get
            {
                return ((EInvoiceType[])Enum.GetValues(typeof(EInvoiceType)))
                     .Select(c => (int)c)
                     .ToList();
            }
        }
    }
}
