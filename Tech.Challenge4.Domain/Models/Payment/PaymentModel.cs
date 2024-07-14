namespace Tech.Challenge4.Domain.Models.Payment
{
    public class PaymentModel
    {
        public int CustomerId { get; set; }
        public int ReservationId { get; set; }
        public decimal ReservationPaymentValue { get; set; }
    }
}
