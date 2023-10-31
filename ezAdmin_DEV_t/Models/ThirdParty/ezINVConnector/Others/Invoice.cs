using Models.Models.Others;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Models.ThirdParty.ezINVConnector.Others
{
    public class Invoice
    {
        public Guid? ezInvoiceId { get; set; }
        [Required]
        public string? FormNo { get; set; }
        [Required]
        public string? Serial { get; set; }
        public long? InvoiceNo { get; set; }
        public string? InvoiceDate { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public string? CustomerTax { get; set; }
        public string? CustomerAddress { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CompanyName { get; set; }
        public string? BankAccount { get; set; }
        public string? BankName { get; set; }
        public string? CurrencyCode { get; set; }

        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? ExchangeRate { get; set; }

        public string? PaymentMethod { get; set; }
        public string? PaymentBankAccount { get; set; }
        public string? PaymentBankName { get; set; }
        public string? Notice { get; set; }

        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? SubAmount { get; set; }

        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? ServiceRate { get; set; }

        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? ServiceCharge { get; set; }

        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? BeforeTaxAmount { get; set; }

        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? TaxRate { get; set; }

        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? TaxAmount { get; set; }

        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? AfterTaxAmount { get; set; }
        
        public string? sid { get; set; }
        public string? RefID { get; set; } = string.Empty;
        public int? InvoiceType { get; set; } = 1;
        public string? SearchCode { get; set; }
    }
}
