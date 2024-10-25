using API.Payment.Global.Stripe.Domain.Implementation.Interfaces;
using API.Payment.Global.Stripe.Models.Input;
using API.Payment.Global.Stripe.Models.Output;
using Microsoft.AspNetCore.Mvc;

namespace API.Payment.Global.Stripe.Service.Controllers
{
    [Route("v1/payment/stripe")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "stripe")]
    public class StripeController : ControllerBase
    {
        private readonly ILogger<StripeController> _logger;

        private readonly IStripeService _stripeService;

        public StripeController(
                ILogger<StripeController> logger,
                IStripeService stripeService
            )
        {
            _logger = logger;
            _stripeService = stripeService;
        }

        [HttpPost("session")]
        public async Task<ActionResult<StripeCreateSessionOutput>> CreateSession([FromBody] StripeCreateSessionInput resource, CancellationToken cancellationToken)
        {
            var Data = _stripeService.CreateSession(resource, cancellationToken);

            if (Data == null)
                return BadRequest();

            return Ok(Data);
        }

        [HttpGet("session/all/{uidInvoincing}")]
        public async Task<ActionResult<List<StripeSessionOutput>>> ListAllSession(CancellationToken cancellationToken)
        {
            var Data = await _stripeService.ListAllSession(cancellationToken);

            return Ok(Data);
        }

        [HttpGet("session/{idSession}/{uidInvoincing}")]
        public async Task<ActionResult<List<StripeSessionOutput>>> ListSessionById(string idSession, CancellationToken cancellationToken)
        {
            var Data = await _stripeService.ListSessionById(idSession, cancellationToken);

            return Ok(Data);
        }

        [HttpPut("session/{idSession}/expire/{uidInvoincing}")]
        public async Task<ActionResult<List<StripeSessionOutput>>> ExpireSessionById(string idSession, CancellationToken cancellationToken)
        {
            var Data = await _stripeService.ExpireSessionById(idSession, cancellationToken);

            return Ok(Data);
        }

        [HttpGet("check/payment/intents/{idPaymentIntents}/{uidInvoincing}")]
        public async Task<ActionResult<StripeCheckingPaymentOutput>> GetStatusPaymentIntents(string idPaymentIntents, CancellationToken cancellationToken)
        {
            var Data = await _stripeService.GetStatusPaymentIntents(idPaymentIntents, cancellationToken);

            return Ok(Data);
        }
    }
}
