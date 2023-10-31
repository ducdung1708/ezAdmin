using Models.ThirdParty.ezINVConnector.Others;

namespace Models.ThirdParty.ezINVConnector.Request
{
    public class ezINVC_CreateInvoiceRequest
    {
        public Invoice Invoice { get; set; }

        public List<InvoiceDetail> Details { get; set; }

        public List<ServiceSummary> ServiceSummarys { get; set; }

        public List<SpecialTaxSummary> SpecialTaxSummarys { get; set; }

        public List<TaxSummary> TaxSummarys { get; set; }


        public int? TypeEinvoice { get; set; }

    }
}
