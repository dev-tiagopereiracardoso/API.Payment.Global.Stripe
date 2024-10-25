using API.Payment.Global.Stripe.Domain.Implementation.Interfaces;
using API.Payment.Global.Stripe.Models.Input;
using API.Payment.Global.Stripe.Models.Output;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Stripe;
using Stripe.Checkout;

namespace API.Payment.Global.Stripe.Domain.Implementation.Services
{
    public class StripeService : IStripeService
    {
        private readonly ILogger<StripeService> _logger;

        private readonly IConsumerService _consumerService;

        private string _apiKeyStripe { set; get; }

        private string _urlBase { set; get; }

        public StripeService(
                ILogger<StripeService> logger,
                IConsumerService consumerService,
                IConfiguration configuration
            )
        {
            _logger = logger;
            _urlBase = "https://api.stripe.com/v1/";
            _apiKeyStripe = configuration["ApiKeyStripe"]!;

            _consumerService = consumerService;
        }

        public StripeCreateSessionOutput? CreateSession(StripeCreateSessionInput resource, CancellationToken cancellationToken)
        {
            StripeConfiguration.ApiKey = _apiKeyStripe;

            try
            {
                var options = new SessionCreateOptions
                {
                    SuccessUrl = resource.Routes.OriginDomain + resource.Routes.SuccessUrl,
                    CancelUrl = resource.Routes.OriginDomain + resource.Routes.CancelUrl,
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                    CustomerEmail = resource.Invoice.CustomerEmail,
                    Metadata = new Dictionary<string, string>
                {
                    { "order_id", resource.Invoice.Id }
                },
                    PaymentMethodTypes = new List<string>()
                {
                    "card"
                }
                };

                foreach (var product in resource.Products)
                {
                    var sessionListItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)product.Price,
                            Currency = "EUR",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = product.Name
                            }
                        },
                        Quantity = product.Quantity
                    };

                    options.LineItems.Add(sessionListItem);
                }

                var service = new SessionService();
                var session = service.Create(options);

                return new StripeCreateSessionOutput()
                {
                    InvoiceId = resource.Invoice.Id,
                    PaymentUrl = session.Url,
                    SessionId = session.Id
                };
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex.Message);

                return null;
            }
        }

        public Task<List<StripeSessionOutput>> ListAllSession(CancellationToken cancellationToken)
        {
            return _consumerService.GetHttpClientMultiple<StripeSessionOutput>(_apiKeyStripe, _urlBase, "checkout/sessions", "data")!;
        }

        public Task<StripeSessionOutput> ExpireSessionById(string idSession, CancellationToken cancellationToken)
        {
            return _consumerService.GetHttpClientSingle<StripeSessionOutput>(_apiKeyStripe, _urlBase, $"checkout/sessions/{idSession}/expire", HttpMethod.Post)!;
        }

        public Task<StripeSessionOutput> ListSessionById(string idSession, CancellationToken cancellationToken)
        {
            return _consumerService.GetHttpClientSingle<StripeSessionOutput>(_apiKeyStripe, _urlBase, $"checkout/sessions/{idSession}")!;
        }

        public Task<StripeCheckingPaymentOutput> GetStatusPaymentIntents(string idPaymentIntents, CancellationToken cancellationToken)
        {
            return _consumerService.GetHttpClientSingle<StripeCheckingPaymentOutput>(_apiKeyStripe, _urlBase, $"payment_intents/{idPaymentIntents}")!;
        }
    }
}