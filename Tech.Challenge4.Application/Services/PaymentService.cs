using FluentValidation;
using Tech.Challenge4.Application.Contracts.Repositories;
using Tech.Challenge4.Domain.Contracts.Services.Payments;
using Tech.Challenge4.Domain.Enumerables;
using Tech.Challenge4.Domain.Models.Payment;

namespace Tech.Challenge4.Application.Services
{
    public class PaymentService : IPaymentsService
    {
        private readonly IReservaRepository _reservaRepository;

        public PaymentService(
            IReservaRepository reservaRepository)
        {
            _reservaRepository = reservaRepository;
        }

        public async Task<bool> ReservationPayment(PaymentModel paymentModel)
        {
            var reservationResult = await _reservaRepository.GetById(paymentModel.ReservationId);

            if (reservationResult is null)
                throw new ValidationException("Reserva não encontrada");

            if (reservationResult.Valor != paymentModel.ReservationPaymentValue)
                throw new ValidationException("Não é possível realizar um pagamento diferente do que foi reservado");

            reservationResult.DataPagamento = DateTime.Now;
            reservationResult.StatusReserva = StatusReserva.Confirmada;
            reservationResult.StatusPagamento = StatusPagamento.Concluido;

            await _reservaRepository.Put(reservationResult);

            return true;
        }
    }
}
