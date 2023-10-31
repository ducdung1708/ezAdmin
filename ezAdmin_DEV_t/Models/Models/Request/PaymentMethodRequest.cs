using Models.Models.Others;

namespace Models.Models.Request
{
    public class PaymentMethodRequest
    {
        public BaseFieldFilter<int?>? Partner { get; set; }
    }
}
