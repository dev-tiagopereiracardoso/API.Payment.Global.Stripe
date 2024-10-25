namespace API.Payment.Global.Stripe.Models.Output
{
    public class StripeCheckingPaymentOutput
    {
        public string Id { get; set; }

        public long Amount { get; set; }

        public string Status { get; set; }
    }
}