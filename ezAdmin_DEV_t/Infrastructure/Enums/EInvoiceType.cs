namespace Infrastructure.Enums
{
    public enum EInvoiceType
    {
        /// <summary>
        /// Hóa đơn điện tử tạo từ Menu Einvoice - Hóa đơn điện tử dùng HSM
        /// </summary>
        INVOICE_NORMAL = 0,
        /// <summary>
        /// Hóa đơn điện tử tạo từ Máy tính tiền dùng HSM
        /// </summary>
        INVOICE_MTT = 1,
		/// <summary>
		/// Hóa đơn điện tử tạo từ Menu Einvoice - Hóa đơn điện tử dùng chữ ký USB Token
		/// </summary>
		INVOICE_NORMAL_USB_TOKEN = 2,
		/// <summary>
		/// Hóa đơn điện tử tạo từ Máy tính tiền dùng chữ ký USB Token
		/// </summary>
		INVOICE_MTT_USB_TOKEN = 3,
    }
}
