namespace Infrastructure.ConstantsDefine.EndPoint
{
    public class ezINVEndpoint
    {
        public const string HEALTHY_CHECK = "api/ezInvoice/HealthCheck";
        public const string CREATE_INVOICE = "api/ezInvoice/Create";
        public const string CREATE_INVOICE_DRAFT = "api/ezInvoice/Draft";
        public const string CREATE_AND_RELEASE = "api/ezInvoice/CreateAndPublishInvoice";
        public const string MULTI_CREATE_AND_RELEASE = "api/ezInvoice/CreateAndPublishMultiInvoice";
        public const string PREVIEW = "api/ezInvoice/Preview";
        public const string CHANGE = "api/ezInvoice/Change";
        public const string DELETE = "api/ezInvoice/Delete";
        public const string VOID_INVOICE = "api/ezInvoice/VoidInvoice";
        public const string RELEASE = "api/ezInvoice/Publish";
        public const string REPLACE_INVOICE = "api/ezInvoice/ReplaceInvoice";
        public const string ADJUST_INVOICE = "api/ezInvoice/AdjustInvoice";
        public const string ADJUST_INCREASE_INVOICE = "api/ezInvoice/AdjustIncreaseInvoice";
        public const string ADJUST_DECREASE_INVOICE = "api/ezInvoice/AdjustDecreaseInvoice";
        public const string ADJUST_INFOR_INVOICE = "api/ezInvoice/AdjustInforInvoice";
        public const string DOWNLOAD_PDF = "api/ezInvoice/DownloadPDF";
        public const string DOWNLOAD_PDF_Invoice_Draft = "api/ezInvoice/DownloadPDFInvoiceDraft";
        public const string SEND_EMAIL = "api/ezInvoice/SendEmail";
        public const string SEND_INVOICE_CQT = "api/ezInvoice/SendInvoiceCQT";
        public const string GET_STATUS_INVOICE = "api/ezInvoice/GetStatusInvoice";
        public const string GET_XML_HASH_INVOICE = "api/ezInvoice/GetXMLHashInvoice";
        public const string SEND_INVOICE_CQT_USB_TOKEN = "api/ezInvoice/SendInvoiceCQTUSBToken";
    }
}
