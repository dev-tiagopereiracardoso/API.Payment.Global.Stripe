namespace API.Payment.Global.Stripe.Models.Input
{
    public class StripeCreateSessionInput
    {
        public StripeCreateSessionRouteInput Routes { set; get; }

        public StripeCreateSessionInvoiceInput Invoice { set; get; }

        public List<StripeCreateSessionProductsInput> Products { set; get; }
    }

    public class StripeCreateSessionRouteInput
    {
        public string OriginDomain { get; set; }

        public string SuccessUrl { get; set; }

        public string CancelUrl { get; set; }
    }

    public class StripeCreateSessionInvoiceInput
    {
        public string Uid { set; get; }

        public string Id { set; get; }

        public string CustomerEmail { set; get; }
    }

    public class StripeCreateSessionProductsInput
    {
        public string Name { set; get; }

        public long Price { set; get; }

        public int Quantity { set; get; }
    }
}
