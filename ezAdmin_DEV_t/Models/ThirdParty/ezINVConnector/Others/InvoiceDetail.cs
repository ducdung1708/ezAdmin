using Models.Models.Others;
using Newtonsoft.Json;

namespace Models.ThirdParty.ezINVConnector.Others
{
    public class InvoiceDetail
    {
        public string? transdate { get; set; }
        public string? ItemName { get; set; }
        public string? UnitName { get; set; }

        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? Quantity { get; set; }

        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? Price { get; set; }

        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? DiscountAmount { get; set; }

        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? DiscountPercent { get; set; }

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

        public string? Note { get; set; }
    }
}
