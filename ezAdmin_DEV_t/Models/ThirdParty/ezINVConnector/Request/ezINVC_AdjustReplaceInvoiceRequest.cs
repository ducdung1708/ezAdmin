using Models.ThirdParty.ezINVConnector.Others;

namespace Models.ThirdParty.ezINVConnector.Request
{
    public class ezINVC_AdjustReplaceInvoiceRequest: ezINVC_CreateInvoiceRequest
    {
        public string? oldezInvoiceId { get; set; }
        public string? oldThirdPartyInvoiceNumber { get; set; }
        public Invoice InvoiceOld { get; set; }
    }
}
