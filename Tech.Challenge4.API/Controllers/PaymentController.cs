using Microsoft.AspNetCore.Mvc;
using Tech.Challenge4.Domain.Contracts.Services.Payments;
using Tech.Challenge4.Domain.Models.Payment;

namespace Tech.Challenge4.API.Controllers
{
    /// <summary>
    /// Controller para gerenciar pagamentos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentsService _paymentsService;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="PaymentController"/>.
        /// </summary>
        public PaymentController(IPaymentsService paymentsService)
        {
            _paymentsService = paymentsService;
        }

        /// <summary>
        /// Realiza o pagamento de uma reserva.
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> ReservationPayment([FromBody] PaymentModel paymentModel)
        {
            var result = await _paymentsService.ReservationPayment(paymentModel);

            return Ok(result);
        }
    }
}
