using Models.Models.Others;
using Newtonsoft.Json;

namespace Models.ThirdParty.ezINVConnector.Others
{
    public class TaxSummary
    {
        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? BeforeTaxAmount { get; set; }

        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? TaxRate { get; set; }

        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? TaxAmount { get; set; }
    }
}
