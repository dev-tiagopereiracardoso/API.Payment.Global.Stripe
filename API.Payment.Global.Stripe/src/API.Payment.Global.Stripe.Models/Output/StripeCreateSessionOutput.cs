namespace API.Payment.Global.Stripe.Models.Output
{
    public class StripeCreateSessionOutput
    {
        public string? InvoiceId { get; set; }

        public string? SessionId { get; set; }

        public string? PaymentUrl { get; set; }
    }
}