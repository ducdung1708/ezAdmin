using Models.Models.Others;
using Newtonsoft.Json;

namespace Models.ThirdParty.ezINVConnector.Others
{
    public class ServiceSummary
    {
        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? ServiceRate { get; set; }

        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? ServiceCharge { get; set; }

        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? TaxRate { get; set; }

        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? TaxAmount { get; set; }

        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? AfterTaxAmount { get; set; }
    }
}
