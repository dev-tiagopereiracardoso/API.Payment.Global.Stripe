using API.Payment.Global.Stripe.Models.Input;
using API.Payment.Global.Stripe.Models.Output;

namespace API.Payment.Global.Stripe.Domain.Implementation.Interfaces
{
    public interface IStripeService
    {
        StripeCreateSessionOutput? CreateSession(StripeCreateSessionInput resource, CancellationToken cancellationToken);

        Task<List<StripeSessionOutput>> ListAllSession(CancellationToken cancellationToken);

        Task<StripeSessionOutput> ListSessionById(string idSession, CancellationToken cancellationToken);

        Task<StripeSessionOutput> ExpireSessionById(string idSession, CancellationToken cancellationToken);

        Task<StripeCheckingPaymentOutput> GetStatusPaymentIntents(string idPaymentIntents, CancellationToken cancellationToken);
    }
}