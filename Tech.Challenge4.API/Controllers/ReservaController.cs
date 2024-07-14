using Microsoft.AspNetCore.Mvc;
using Tech.Challenge4.Domain.Contracts.Services.Reservas;
using Tech.Challenge4.Domain.Models.Reserva;

namespace Tech.Challenge4.API.Controllers
{
    /// <summary>
    /// Controller para gerenciar reservas.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ReservaController : ControllerBase
    {
        private readonly IReservaService _reservaService;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="ReservaController"/>.
        /// </summary>
        public ReservaController(IReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        /// <summary>
        /// Efetua uma nova reserva.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> EfetuarReserva(ReservaModel reservaModel)
        {
            var result = await _reservaService.EfetuarReserva(reservaModel);

            return Ok(result);
        }

        /// <summary>
        /// Obtém uma reserva pelo ID.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _reservaService.GetById(id);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Obtém todas as reservas.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _reservaService.GetAll());
        }

        /// <summary>
        /// Cancela uma reserva pelo ID.
        /// </summary>
        [HttpPut("Cancelation/{reservationId:int}")]
        public async Task<IActionResult> ReservationPayment(int reservationId)
        {
            var result = await _reservaService.CancelReservation(reservationId);

            return Ok(result);
        }

        /// <summary>
        /// Registra a presença em uma reserva pelo ID.
        /// </summary>
        [HttpPut("Present/{reservationId:int}")]
        public async Task<IActionResult> PresentReservation(int reservationId)
        {
            var result = await _reservaService.PresentReservation(reservationId);

            return Ok(result);
        }
    }
}
