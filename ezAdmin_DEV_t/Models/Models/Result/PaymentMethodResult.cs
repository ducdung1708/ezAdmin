using System.ComponentModel.DataAnnotations;

namespace Models.Models.Result
{
    public class PaymentMethodResult
    {
        public string? PaymentCode { get; set; }
        public string? PaymentDescription { get; set; }
        public int PartnerId { get; set; }
    }
}
