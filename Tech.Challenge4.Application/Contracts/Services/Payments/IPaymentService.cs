using Tech.Challenge4.Domain.Models.Payment;

namespace Tech.Challenge4.Domain.Contracts.Services.Payments
{
    public interface IPaymentsService
    {
        Task<bool> ReservationPayment(PaymentModel paymentModel);
    }
}
