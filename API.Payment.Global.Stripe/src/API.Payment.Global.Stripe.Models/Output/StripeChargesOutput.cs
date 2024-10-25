using Newtonsoft.Json;

namespace API.Payment.Global.Stripe.Models.Output
{
    public class StripeChargesOutput
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("object")]
        public string? Object { get; set; }

        [JsonProperty("amount")]
        public long? Amount { get; set; }

        [JsonProperty("amount_captured")]
        public long? AmountCaptured { get; set; }

        [JsonProperty("amount_refunded")]
        public long? AmountRefunded { get; set; }

        [JsonProperty("application_fee_amount")]
        public long? ApplicationFeeAmount { get; set; }

        [JsonProperty("authorization_code")]
        public string? AuthorizationCode { get; set; }

        [JsonProperty("calculated_statement_descriptor")]
        public string? CalculatedStatementDescriptor { get; set; }

        [JsonProperty("captured")]
        public bool? Captured { get; set; }

        [JsonProperty("created")]
        public long? Created { get; set; }

        [JsonProperty("currency")]
        public string? Currency { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("disputed")]
        public bool? Disputed { get; set; }

        [JsonProperty("failure_code")]
        public string? FailureCode { get; set; }

        [JsonProperty("failure_message")]
        public string? FailureMessage { get; set; }

        [JsonProperty("livemode")]
        public bool? Livemode { get; set; }

        [JsonProperty("metadata")]
        public Dictionary<string, string>? Metadata { get; set; }

        [JsonProperty("paid")]
        public bool? Paid { get; set; }

        [JsonProperty("payment_method")]
        public string? PaymentMethod { get; set; }

        [JsonProperty("payment_intent")]
        public string? PaymentIntent { get; set; }

        [JsonProperty("receipt_email")]
        public string? ReceiptEmail { get; set; }

        [JsonProperty("receipt_number")]
        public string? ReceiptNumber { get; set; }

        [JsonProperty("receipt_url")]
        public string? ReceiptUrl { get; set; }

        [JsonProperty("statement_descriptor")]
        public string? StatementDescriptor { get; set; }

        [JsonProperty("statement_descriptor_suffix")]
        public string? StatementDescriptorSuffix { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("transfer_group")]
        public string? TransferGroup { get; set; }
    }
}
