using Models.Models.Others;
using Newtonsoft.Json;

namespace Models.ThirdParty.ezINVConnector.Others
{
    public class SpecialTaxSummary
    {
        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? BeforeSpecialTaxAmount { get; set; }

        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? SpecialTaxAmount { get; set; }

        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? SpecialTaxRate { get; set; }

        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? TaxAmount { get; set; }

        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? TaxRate { get; set; }

        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? AfterTaxAmount { get; set; }
    }
}
