namespace Infrastructure.ConstantsDefine.KeywordTranslate
{
    public class eInvoiceKeywords
    {
        // Validation
        public const string SiteNotExist = "Site does not exist";
        public const string TokenInvalid = "Token invalid";
        public const string SiteDeacitvated = "Site has been deactivated";
        public const string PartnerNotConnected = "Partner not connected";
        public const string SoftwareNotConnected = "Software not connected";
        public const string InvoiceCreateStatusIncorrect = "InvoiceCreateStatus field incorrect";
        public const string EInvoiceTypeIncorrect = "EInvoiceType field incorrect";
        public const string InvoiceTypeIncorrect = "InvoiceType field incorrect";
        // Success
        public const string CreateInvoiceSuccess = "Create invoice success";
        public const string DeleteInvoiceSuccess = "Delete invoice success";
        public const string VoidInvoiceSuccess = "Void invoice success";
        public const string DownloadPDFSuccess = "Download PDF success";
        public const string CreateAndReleaseInvoiceSuccess = "Create and release invoice success";
        public const string SendTaxAdministrationSuccess = "Send Tax Administration success";
        public const string GetInvoiceStatusSendCQTSuccess = "Get Invoice status send Tax Administration success";
        // Error
        public const string ProcessDataError = "Server process data error";
        public const string CallAPIConnectorFail = "Request service partner error";
        public const string DownloadPDFFail = "Download PDF fail";
        public const string CreateInvoiceFail = "Create invoice fail";
        public const string CreateAndReleaseInvoiceFail = "Create and release invoice fail";
        
    }
}
